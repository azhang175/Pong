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
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        OriginalBall.gameObject.SetActive(true);
        OriginalBall.StartBall();

        Debug.Log("Game Started");


        Debug.Log("Restart button active: " + restartButton.gameObject.activeSelf);
        Debug.Log("Main menu button active: " + mainMenuButton.gameObject.activeSelf);
    }

    public IEnumerator StartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Pong");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title Screen");
    }
}
