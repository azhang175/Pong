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

    public TMP_Text explosionTimerTextUI;
    public int explosionTimer = 30;

    public TMP_Text playerWinText;

    public bool gameEnded = false;

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

    public void OnScoreZoneReached(Ball scoringBall, int id)
    {
        if(gameEnded)
        {
            return;
        }

        if (id == 2)
        {
            scorePlayer1++;
        }
        
        if (id == 1)
        {
            scorePlayer2++;
        }

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

    private void CheckWin()
    {
        int winnerID = scorePlayer1 == MaxScore ? 1 : scorePlayer2 == MaxScore ? 2 : 0;
        if (winnerID != 0)
        {
            gameAudio.PlayWinSound();


            gameEnded = true;
            playerWinText.text = "Player " + winnerID + " wins!";

            restartButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true); 
            
            Debug.Log("Player " + winnerID + " wins!");

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
    }

    public void StartExplosionTimer(float delay)
    {
        StartCoroutine(ExplosionTimer());
    }

    private IEnumerator ExplosionTimer()
    {
        float currentTime = explosionTimer;
        while(currentTime > 0)
        {
            explosionTimerTextUI.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        explosionTimerTextUI.text = "Boom!";
        yield return new WaitForSeconds(1f);
        explosionTimerTextUI.gameObject.SetActive(false);

        OriginalBall.Explode();
    }

}
