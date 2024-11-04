using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityPowerUp : MonoBehaviour
{
   public GameManager gameManager;
   public float duration = 5f;
    public float lifetime = 7f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.GetComponent<Ball>();

        if (ball != null)
        {
            ball.StartCoroutine(ball.MakeInvisible(duration));

            Destroy(gameObject);
        }
    }
}
