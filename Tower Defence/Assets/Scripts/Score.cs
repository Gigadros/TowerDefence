using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public string username = "no name";
    public int score = 0;
    public int gold = 100;
    public int lives = 10;
    public Text scoreText, goldText, waveText, livesText;
    public GameObject GameOverScreen, PauseScreen, SavedText;
    bool isPaused = false, hasBeenSaved = false;

    public void UpdateName(string newName)
    {
        username = newName;
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        GameOverScreen.SetActive(true);
    }

    public void Upload()
    {
        if (!hasBeenSaved)
        {
            HSController.current.PostHS();
            SavedText.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameOverScreen.activeSelf)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0f;
                PauseScreen.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                PauseScreen.SetActive(false);
            }
        }
        scoreText.text = score.ToString();
        goldText.text = gold.ToString();
        waveText.text = GameObject.FindObjectOfType<Spawn>().waveCount.ToString();
        livesText.text = lives.ToString();
        if(lives  <= 0)
        {
            GameOver();
        }
    }
}
