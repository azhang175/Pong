using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Manages the audio for the game, including playing sounds for wall hits, paddle hits, scoring, and winning.
public class GameAudio : MonoBehaviour
{
    // The audio source component used to play the audio clips.
    public AudioSource audioSource;

    // The audio clip to play when the ball hits the wall.
    public AudioClip wallSound;

    // The audio clip to play when the ball hits the paddle.
    public AudioClip paddleSound;

    // The audio clip to play when a player scores.
    public AudioClip scoreSound;

    // The audio clip to play when a player wins.
    public AudioClip winSound;

    // Plays the wall hit sound.
    public void PlayWallSound()
    {
        audioSource.PlayOneShot(wallSound);
    }

    // Plays the paddle hit sound.
    public void PlayPaddleSound()
    {
        audioSource.PlayOneShot(paddleSound);
    }

    // Plays the score sound.
    public void PlayScoreSound()
    {
        audioSource.PlayOneShot(scoreSound);
    }

    // Plays the win sound.
    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound);
    }
}

