using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        Debug.Log("Start button clicked");
        SceneManager.LoadScene("Pong");
    }
}
