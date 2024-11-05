using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public GameManager gameManager;
    public float speedMultiplier = 1.5f;
    public float duration = 5f;
    public float lifetime = 7f;

    // This method is called when another collider enters the trigger collider attached to the object where this script is attached
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        Ball ball = collision.GetComponent<Ball>();

        if (ball != null)
        {
            // Start the coroutine to change the ball's speed for a certain duration
            StartCoroutine(ball.ChangeSpeed(speedMultiplier, duration));

            // Destroy the power-up object after it has been used
            Destroy(gameObject);
        }
    }
}
