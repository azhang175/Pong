using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// Controls the movement of the paddle in the game.
public class Paddle : MonoBehaviour
{
    /// Reference to the Rigidbody2D component.
    public Rigidbody2D rd2d;

    /// Speed at which the paddle moves.
    public float moveSpeed = 2f;

    /// Identifier for the paddle (1 or 2).
    public int id;

    /// Minimum Y position the paddle can move to.
    public float minY = -4.5f;

    /// Maximum Y position the paddle can move to.
    public float maxY = 4.5f;

    /// Called once per frame to update the paddle's position.
    private void Update()
    {
        float movement = ProcessInput();
        Move(movement);
    }

    /// Processes the input for the paddle based on its identifier.
    /// <returns>The movement value based on player input.</returns>
    private float ProcessInput()
    {
        float movement = 0f;
        switch (id)
        {
            case 1:
                movement = Input.GetAxis("Player1");
                break;
            case 2:
                movement = Input.GetAxis("Player2");
                break;
        }
        return movement;
    }

    /// Moves the paddle based on the input movement value.
    /// <param name="movement">The movement value to apply to the paddle.</param>
    private void Move(float movement)
    {
        Vector2 velocity = rd2d.velocity;
        velocity.y = movement * moveSpeed;
        rd2d.velocity = velocity;

        Vector2 position = transform.position;
        position.y = Mathf.Clamp(position.y, minY, maxY);
        transform.position = position;
    }
}

