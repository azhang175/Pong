using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityPowerUp : MonoBehaviour
{
    public GameManager gameManager;
    public AudioSource audioSource;
    public AudioClip powerUpSound;
    public float duration = 5f;
    public float lifetime = 7f;

    // Triggered when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.GetComponent<Ball>();

        if (ball != null)
        {
            // Start the coroutine to make the ball invisible for a specified duration
            ball.StartCoroutine(ball.MakeInvisible(duration));

            // Destroy the power-up object after it has been used
            Destroy(gameObject);
        }
    }
}
