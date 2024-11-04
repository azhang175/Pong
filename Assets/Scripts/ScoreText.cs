using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void SetScore(int score){
        scoreText.text = score.ToString();
    }
}
