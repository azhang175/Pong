using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenUI : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        Debug.Log("Start button clicked");
        SceneManager.LoadScene("Pong");
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();
    }
    
}
