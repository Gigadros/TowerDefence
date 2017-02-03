using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public int score = 0;
    public int gold = 100;
    public Text scoreText, goldText;

    void Update()
    {
        scoreText.text = score.ToString();
        goldText.text = gold.ToString();
    }
}
