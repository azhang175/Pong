using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameAudio gameAudio;
    public GameController gameController;
    public Ball OriginalBall;
    public Button restartButton;
    public Button mainMenuButton;
    public int scorePlayer1, scorePlayer2;
    public ScoreText scoreTextPlayer1, scoreTextPlayer2;
    public int MaxScore = 5;
    public TMP_Text countdownTextUI;
    public int countdownTime = 3;
    public TMP_Text playerWinText;
    public TMP_Text timerText;
    public bool gameEnded = false;
    public bool timerStarted = false;
    public float elapsedTime = 0f;
    public float timerDuration = 60f;
    
    

    private void Start()
    {
        if(OriginalBall == null)
        {
            Debug.LogError("OriginalBall is not set in the GameManager");
        }
        else
        {
            StartCoroutine(StartCountdown());
        }

        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);

    }

    private void Update()
    {
        timerText.text = "Time: " + timerDuration.ToString("F2");

        if(!gameEnded && timerStarted)
        {
            elapsedTime += Time.deltaTime;

            float remainingTime = timerDuration - elapsedTime;
            timerText.text = "Time: " + Mathf.Max(remainingTime, 0f).ToString("F2");

            if(elapsedTime >= timerDuration)
            {
                CheckWin();
            }
        }
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        timerStarted = true;
    }


    public void OnScoreZoneReached(Ball scoringBall, int id)
    {
        if(gameEnded)
        {
            return;
        }

        if (id == 2)
        {
            scorePlayer1++;
            Debug.Log("Player 1 Scored!" + scorePlayer1);
        }
        
        if (id == 1)
        {
            scorePlayer2++;
            Debug.Log("Player 2 Scored!" + scorePlayer2);
        }

        HighlightScore(id);
        UpdateScore();

        if(scoringBall == OriginalBall)
        {
            OriginalBall.ResetSpeed();
            OriginalBall.resetBall();
        }
        CheckWin();
    }

    private void UpdateScore()
    {
        scoreTextPlayer1.SetScore(scorePlayer1);
        scoreTextPlayer2.SetScore(scorePlayer2);
    }

    public void HighlightScore(int id)
    {
        if(id == 2)
        {
            scoreTextPlayer1.Highlight();
        }
        else
        {
            scoreTextPlayer2.Highlight();
        }
    }



    private void CheckWin()
    {
        int winnerID = scorePlayer1 == MaxScore ? 1 : scorePlayer2 == MaxScore ? 2 : 0;
        if (winnerID == 0 && elapsedTime >= timerDuration)
        {
            if (scorePlayer1 > scorePlayer2)
            {
                winnerID = 1;
            }
            else if (scorePlayer2 > scorePlayer1)
            {
                winnerID = 2;
            }
            else
            {
                winnerID = -1; // Indicates a tie
            }
        }
        
        if (winnerID != 0)
        {
            gameAudio.PlayWinSound();

            
            gameEnded = true;

            if(winnerID == -1)
            {
                playerWinText.text = "It's a tie!";
                Debug.Log("It's a tie!");
            }
            else
            {        
               playerWinText.text = "Player " + winnerID + " Wins!";
               Debug.Log("Player " + winnerID + " wins!");
            }

            restartButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true); 

            OriginalBall.StopBall();
        }
        else
        {
            gameAudio.PlayScoreSound();
        }

        
    }


    private IEnumerator StartCountdown()
    {
        float currentTime = countdownTime;
        while(currentTime > 0)
        {
            countdownTextUI.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        countdownTextUI.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownTextUI.gameObject.SetActive(false);
        OriginalBall.StartBall();

        StartCoroutine(gameController.StartGameAfterDelay(4f));

        StartTimer(); // Initialize the timer after the countdown and delay
    }


}
