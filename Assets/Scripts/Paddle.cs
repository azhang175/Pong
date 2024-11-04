using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rd2d;
    public float moveSpeed = 2f;
    public int id;

    private void Update()
    {
        float movement = ProcessInput();
        Move(movement);
    }

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

    private void Move(float movement)
    {
        Vector2 velocity = rd2d.velocity;
        velocity.y = movement * moveSpeed;
        rd2d.velocity = velocity;
    }

    
}
