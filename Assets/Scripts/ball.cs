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

  // Called when the script instance is being loaded
  private void Awake()
  {
    gameManager = FindObjectOfType<GameManager>();
    originalSpeed = moveSpeed;
  }

  // Starts the ball movement
  public void StartBall()
  {
    InitialPush();
  }

  // Stops the ball movement
  public void StopBall()
  {
    rb2b.velocity = Vector2.zero;
    rb2b.isKinematic = true;
    rb2b.simulated = false;
  }

  // Applies an initial push to the ball in a random direction
  private void InitialPush()
  {
    Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;
    dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);
    rb2b.velocity = dir * moveSpeed;
  }

  // Resets the ball speed to its original value
  public void ResetSpeed()
  {
    moveSpeed = originalSpeed;
    rb2b.velocity = rb2b.velocity.normalized * moveSpeed;
  }

  // Resets the ball position to a random Y position and stops its movement
  public void resetBall()
  {
    float posY = Random.Range(-maxStartY, maxStartY);
    Vector2 position = new Vector2(startX, posY);
    transform.position = position;
    rb2b.velocity = Vector2.zero;
  }

  // Called when the ball enters a trigger collider
  private void OnTriggerEnter2D(Collider2D collision)
  {
    ScoreZone scoreZone = collision.GetComponent<ScoreZone>();

    if (scoreZone != null)
    {
      gameManager.OnScoreZoneReached(this, scoreZone.id);
      resetBall();
      ResetSpeed();
      StartBall();
    }
  }

  // Called when the ball collides with another collider
  private void OnCollisionEnter2D(Collision2D collision)
  {
    Paddle paddle = collision.gameObject.GetComponent<Paddle>();
    if (paddle)
    {
      gameManager.gameAudio.PlayPaddleSound();
    }

    Wall wall = collision.gameObject.GetComponent<Wall>();
    if (wall)
    {
      gameManager.gameAudio.PlayWallSound();
    }
  }

  // Temporarily changes the ball speed for a given duration
  public IEnumerator ChangeSpeed(float multiplier, float duration)
  {
    moveSpeed *= multiplier;
    rb2b.velocity = rb2b.velocity.normalized * moveSpeed;

    yield return new WaitForSeconds(duration);

    ResetSpeed();
  }

  // Sets the visibility of the ball
  public void SetVisibility(bool isVisible)
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    if (spriteRenderer != null)
    {
      spriteRenderer.enabled = isVisible;
    }
    else
    {
      Debug.LogError("SpriteRenderer is not assigned in Ball");
    }
  }

  // Makes the ball invisible for a given duration
  public IEnumerator MakeInvisible(float duration)
  {
    SetVisibility(false);
    yield return new WaitForSeconds(duration);
    SetVisibility(true);
  }

  // Changes the ball direction to a random direction
  public void ChangeDirectionRandomly()
  {
    Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    rb2b.velocity = randomDirection * moveSpeed;
  }

  // Periodically changes the ball direction at random intervals
  private IEnumerator ChangeDirectionPeriodically()
  {
    while (true)
    {
      yield return new WaitForSeconds(Random.Range(2f, 5f));
      ChangeDirectionRandomly();
    }
  }
}
