using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    GameManager gameManager;

    public Ball OriginalBall;

    public Button restartButton;

    public Button mainMenuButton;

    void Start()
    {
        // Hide the restart and main menu buttons at the start of the game
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }

    void StartGame()
    {
        // Activate and start the ball
        OriginalBall.StartBall();

        Debug.Log("Game Started");

        Debug.Log("Restart button active: " + restartButton.gameObject.activeSelf);
        Debug.Log("Main menu button active: " + mainMenuButton.gameObject.activeSelf);
    }

    public IEnumerator StartGameAfterDelay(float delay)
    {
        // Wait for the specified delay before starting the game
        yield return new WaitForSeconds(delay);
        StartGame();
    }

    public void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene("Pong");
    }

    public void MainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("Title Screen");
    }
}
