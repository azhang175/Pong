using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public GameManager gameManager;
    public float speedMultiplier = 1.5f;
    public float duration = 5f;
    public float lifetime = 7f;

  

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        Ball ball = collision.GetComponent<Ball>();

        if (ball != null)
        {
            StartCoroutine(ball.ChangeSpeed(speedMultiplier, duration));

            Destroy(gameObject);
        }

        
    }
}
