using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component to display the score
    public TextMeshProUGUI scoreText;
    
    // Reference to the Animator component to handle animations
    public Animator animator;

    // Method to trigger the highlight animation
    public void Highlight()
    {
        animator.SetTrigger("Highlight");
    }
    
    // Method to update the score text
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
