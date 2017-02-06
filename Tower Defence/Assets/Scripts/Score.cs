using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public int score = 0;
    public int gold = 100;
    public int lives = 10;
    public Text scoreText, goldText, waveText, livesText;
    public GameObject GameOverScreen, PauseScreen;
    bool isPaused = false;

    void GameOver()
    {
        Time.timeScale = 0f;
        GameOverScreen.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
