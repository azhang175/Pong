using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSplitPowerUp : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject splitBallPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.GetComponent<Ball>();
        if (ball != null && !ball.isClone)
        {
            SplitBall(ball);
            Destroy(gameObject);
        }


    }

    private void SplitBall(Ball OriginalBall)
    {
        for(int i = 0; i < 2; i++)
        {
        GameObject newBall = Instantiate(splitBallPrefab, OriginalBall.transform.position, Quaternion.identity);
        Rigidbody2D newRb = newBall.GetComponent<Rigidbody2D>();
        Ball newBallScript = newBall.GetComponent<Ball>();
        newBallScript.isClone = true;


        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        newRb.velocity = direction * OriginalBall.moveSpeed;


        }
    }
}
