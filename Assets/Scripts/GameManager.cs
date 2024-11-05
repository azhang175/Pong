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
        // Check if the OriginalBall is set, if not log an error
        if (OriginalBall == null)
        {
            Debug.LogError("OriginalBall is not set in the GameManager");
        }
        else
        {
            // Start the countdown coroutine
            StartCoroutine(StartCountdown());
        }

        // Hide the restart and main menu buttons at the start
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Update the timer text with the remaining time
        timerText.text = "Time: " + timerDuration.ToString("F2");

        // If the game has not ended and the timer has started
        if (!gameEnded && timerStarted)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the remaining time
            float remainingTime = timerDuration - elapsedTime;
            timerText.text = "Time: " + Mathf.Max(remainingTime, 0f).ToString("F2");

            // Check if the timer has reached its duration
            if (elapsedTime >= timerDuration)
            {
                CheckWin();
            }
        }
    }

    public void StartTimer()
    {
        // Reset the elapsed time and start the timer
        elapsedTime = 0f;
        timerStarted = true;
    }

    public void OnScoreZoneReached(Ball scoringBall, int id)
    {
        // If the game has ended, return early
        if (gameEnded)
        {
            return;
        }

        // Increment the score for the respective player
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

        // Highlight the score and update the score display
        HighlightScore(id);
        UpdateScore();

        // Reset the ball if it is the original ball
        if (scoringBall == OriginalBall)
        {
            OriginalBall.ResetSpeed();
            OriginalBall.resetBall();
        }
        CheckWin();
    }

    private void UpdateScore()
    {
        // Update the score text for both players
        scoreTextPlayer1.SetScore(scorePlayer1);
        scoreTextPlayer2.SetScore(scorePlayer2);
    }

    public void HighlightScore(int id)
    {
        // Highlight the score for the respective player
        if (id == 2)
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
        // Determine the winner based on the scores or elapsed time
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

        // If there is a winner or a tie
        if (winnerID != 0)
        {
            gameAudio.PlayWinSound();
            gameEnded = true;

            // Display the winner or tie message
            if (winnerID == -1)
            {
                playerWinText.text = "It's a tie!";
                Debug.Log("It's a tie!");
            }
            else
            {
                playerWinText.text = "Player " + winnerID + " Wins!";
                Debug.Log("Player " + winnerID + " wins!");
            }

            // Show the restart and main menu buttons
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
        // Countdown before the game starts
        float currentTime = countdownTime;

        while (currentTime > 0)
        {
            countdownTextUI.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        countdownTextUI.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownTextUI.gameObject.SetActive(false);

        // Start the ball and the game after the countdown
        OriginalBall.StartBall();
        StartCoroutine(gameController.StartGameAfterDelay(4f));
        StartTimer();
    }
}
