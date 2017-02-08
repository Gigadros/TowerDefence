using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// HSController script adapted from http://wiki.unity3d.com/index.php?title=Server_Side_Highscores
public class HSController : MonoBehaviour {

    public static HSController current;

    private string secretKey = "lt6fedap37";
    public string addScoreURL = "http://localhost/leaderboard/addscore.php?";
    public string highscoreURL = "http://localhost/leaderboard/display.php";
    public bool getHighscoreAtStart = false;
    bool useLocalScores = false;

    List<LocalHS> localScores;

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        if (getHighscoreAtStart)
        {
            GetHS();
        }
    }

    public void ToggleScores(bool useLocal)
    {
        useLocalScores = useLocal;
        GetHS();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        try
        {
            file = File.Open(Application.persistentDataPath + "/highscores.dat", FileMode.Open);
            localScores = (List<LocalHS>)bf.Deserialize(file);
            file.Close();
            LocalHS newScore = new LocalHS(GameObject.FindObjectOfType<Score>().username, GameObject.FindObjectOfType<Score>().score);
            localScores.Add(newScore);
            file = File.Create(Application.persistentDataPath + "/highscores.dat");
            bf.Serialize(file, localScores);
            file.Close();
        }
        catch
        {
            print("There was an error saving local leaderboard. Creating a new file.");
            file = File.Create(Application.persistentDataPath + "/highscores.dat");
            localScores = new List<LocalHS>();
            LocalHS newScore = new LocalHS(GameObject.FindObjectOfType<Score>().username, GameObject.FindObjectOfType<Score>().score);
            localScores.Add(newScore);
            bf.Serialize(file, localScores);
            file.Close();
        }
    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        try
        {
            file = File.Open(Application.persistentDataPath + "/highscores.dat", FileMode.Open);
            List<LocalHS> data = (List<LocalHS>)bf.Deserialize(file);
            file.Close();
            localScores = data;
        }
        catch
        {
            print("There was an error loading local leaderboard. Creating a new file.");
            file = File.Create(Application.persistentDataPath + "/highscores.dat");
            localScores = new List<LocalHS>();
            bf.Serialize(file, localScores);
            file.Close();

            file = File.Open(Application.persistentDataPath + "/highscores.dat", FileMode.Open);
            List<LocalHS> data = (List<LocalHS>)bf.Deserialize(file);
            file.Close();
            localScores = data;
        }
    }

    public void PostHS()
    {
        StartCoroutine(PostScores());
        Save();
    }

    public void GetHS()
    {
        StartCoroutine(GetScores());
        Load();
    }

    public void GetLocalHS()
    {
        localScores.Sort();
        localScores.Reverse();
        string localLeaderboard = "";
        for(int i = 0; i < 5; i++)
        {
           if (localScores.Count <= i) break;
            localLeaderboard += (localScores[i].ToString() + '\n');
        }
        gameObject.GetComponent<Text>().text = localLeaderboard;
    }

    // Post current score to the MySQL DB
    // remember to use StartCoroutine when calling this function!
    IEnumerator PostScores()
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(GameObject.FindObjectOfType<Score>().username + GameObject.FindObjectOfType<Score>().score + secretKey);

        string post_url = addScoreURL + "name=" + WWW.EscapeURL(GameObject.FindObjectOfType<Score>().username) + "&score=" + GameObject.FindObjectOfType<Score>().score + "&hash=" + hash;

        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
    }

    // Get the scores from the MySQL DB to display in a UI Text component
    // remember to use StartCoroutine when calling this function!
    IEnumerator GetScores()
    {
        if (useLocalScores)
        {
            GetLocalHS();
        }
        else
        {
            gameObject.GetComponent<Text>().text = "Loading...";
            WWW hs_get = new WWW(highscoreURL);
            yield return hs_get;

            if (hs_get.error != null)
            {
                gameObject.GetComponent<Text>().text = "Could not retrieve leaderboard.";
                print("There was an error getting the high score: " + hs_get.error);
            }
            else
            {
                gameObject.GetComponent<Text>().text = hs_get.text; // this is a GUIText that will display the scores in game.
            }
        }
    }

    // MD5 code from http://wiki.unity3d.com/index.php/MD5
    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
}

[Serializable]
class LocalHS : IComparable<LocalHS>
{
    public string name;
    public int score;

    public LocalHS(string username, int highscore)
    {
        name = username;
        score = highscore;
    }

    public override string ToString()
    {
        int padding = 28 - score.ToString().Length;
        return name.PadRight(padding) + score;
    }

    public int CompareTo(LocalHS comparePart)
    {
        if (comparePart == null)
            return 1;

        else
            return this.score.CompareTo(comparePart.score);
    }
}