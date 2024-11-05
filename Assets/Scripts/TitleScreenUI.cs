using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenUI : MonoBehaviour
{
    // Called when the Start button is clicked
    public void OnStartButtonClicked()
    {
        Debug.Log("Start button clicked");
        SceneManager.LoadScene("Pong");
    }

    // Called when the Quit button is clicked
    public void OnQuitButtonClicked()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();
    }
}
