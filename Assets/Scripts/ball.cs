using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

public class Ball : MonoBehaviour
{
  public Paddle paddle;
  public Wall wall;


  public GameObject ballPrefab;
  public GameManager gameManager;
  public Rigidbody2D rb2b;
  private SpriteRenderer spriteRenderer;

  public float maxInitialAngle = 0.67f;
  public float moveSpeed = 2f;
  public float maxStartY = 4f;
  public float speedMultiplier = 1.5f;
  public bool isClone = false;

  private float startX = 0f;
  private float originalSpeed;

    
  private void Awake()
  {
    gameManager = FindObjectOfType<GameManager>();
    originalSpeed = moveSpeed;
  }

  public void StartBall()
  {
    // if(gameManager.gameEnded)
    // {
    //   return;
    // }
    InitialPush();
  }

  public void StopBall()
  {
    rb2b.velocity = Vector2.zero;
    rb2b.isKinematic = true;
    rb2b.simulated = false;
  }

  private void InitialPush(){
    Vector2 dir = Random.value<0.5f ? Vector2.left : Vector2.right;

    dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);
    rb2b.velocity = dir * moveSpeed;
  }

  public void ResetSpeed()
  {
    moveSpeed = originalSpeed;
    rb2b.velocity = rb2b.velocity.normalized * moveSpeed;
  }

  public void resetBall(){
    float posY = Random.Range(-maxStartY, maxStartY);
    Vector2 position = new Vector2(startX, posY);
    transform.position = position;

    rb2b.velocity = Vector2.zero;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    ScoreZone scoreZone = collision.GetComponent<ScoreZone>();

    if(scoreZone != null)
    {
      if(isClone)
      {
        gameManager.OnScoreZoneReached(this, scoreZone.id);
        Destroy(gameObject);
      }
      else
      {
        gameManager.OnScoreZoneReached(this, scoreZone.id);
        resetBall();
        ResetSpeed();
        StartBall();
      }
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    Paddle paddle = collision.gameObject.GetComponent<Paddle>();
    if(paddle)
    {
      gameManager.gameAudio.PlayPaddleSound();
    }

    Wall wall = collision.gameObject.GetComponent<Wall>();
    if(wall)
    {
      gameManager.gameAudio.PlayWallSound();
    }

    SpeedPowerUp speedPowerUp = collision.gameObject.GetComponent<SpeedPowerUp>();
    if (speedPowerUp)
    {
        gameManager.gameAudio.PlayPowerUpSound();
    }

    BallSplitPowerUp splitPowerUp = collision.gameObject.GetComponent<BallSplitPowerUp>();
    if (splitPowerUp)
    {
        gameManager.gameAudio.PlayPowerUpSound();
    }
  }

  public IEnumerator ChangeSpeed(float multiplier, float duration)
  {
    moveSpeed *= multiplier;
    rb2b.velocity = rb2b.velocity.normalized * moveSpeed;

    yield return new WaitForSeconds(duration);

    ResetSpeed();
  }

  public void Explode()
  {
    int numberOfBalls = Random.Range(1, 10);
    for(int i = 0; i < numberOfBalls; i++)
    {
      GameObject newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
      Ball newBallScript = newBall.GetComponent<Ball>();
      newBallScript.isClone = true;

      Rigidbody2D newRb2b = newBall.GetComponent<Rigidbody2D>();
      Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * moveSpeed;
      newRb2b.velocity = dir;    
    }

    Destroy(gameObject);
  }

  public void SetVisibility(bool isVisible)
    {
        if (spriteRenderer == null)
        {
            spriteRenderer.enabled = isVisible;
        }
        else
        {
          Debug.LogError("SpriteRenderer is not assigned in Ball");
        }
    }

  public IEnumerator MakeInvisible(float duration)
  {
    SetVisibility(false);
    yield return new WaitForSeconds(duration);
    SetVisibility(true);
  }
  
}
