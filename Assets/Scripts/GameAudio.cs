using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip wallSound;
    public AudioClip paddleSound;
    public AudioClip scoreSound;   
    public AudioClip winSound;
    public AudioClip powerUpSound;

    public void PlayWallSound()
    {
        audioSource.PlayOneShot(wallSound);
    }
    
    public void PlayPaddleSound()
    {
        audioSource.PlayOneShot(paddleSound);
    }

    public void PlayScoreSound()
    {
        audioSource.PlayOneShot(scoreSound);
    }

    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound);
    }

}

