using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSController : MonoBehaviour {

    public static HSController current;

    private string secretKey = "lt6fedap37";
    public string addScoreURL = "http://localhost/leaderboard/addscore.php?";
    public string highscoreURL = "http://localhost/leaderboard/display.php";
    public bool getHighscoreAtStart = false;

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

    public void PostHS()
    {
        StartCoroutine(PostScores());
    }

    public void GetHS()
    {
        StartCoroutine(GetScores());
    }

    // HSController script from http://wiki.unity3d.com/index.php?title=Server_Side_Highscores
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

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    IEnumerator GetScores()
    {
        gameObject.GetComponent<Text>().text = "Loading...";
        WWW hs_get = new WWW(highscoreURL);
        yield return hs_get;

        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            gameObject.GetComponent<Text>().text = hs_get.text; // this is a GUIText that will display the scores in game.
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
