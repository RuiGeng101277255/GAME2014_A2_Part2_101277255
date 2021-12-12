/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: MovablePlatformScript.cs
 Last Modified: December 12th, 2021
 Description: Moving Platform's Behaviours based on Professor Tom's lab.
 Version History: v1.03 Public enum class moved within this script, minor Modification and Internal Documentations
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatformScript : MonoBehaviour
{
    [Header("Movement")]
    public MovableDirection direction;
    [Range(0.1f, 10.0f)]
    public float speed;
    [Range(1, 20)]
    public float distance;
    [Range(0.05f, 0.1f)]
    public float distanceOffset;
    public bool isLooping;

    private Vector2 startingPosition;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
        if (isLooping)
        {
            isMoving = true;
        }
    }

    private void MovePlatform()
    {
        float pingPongValue = (isMoving) ? Mathf.PingPong(Time.time * speed, distance) : distance;

        if ((!isLooping) && (pingPongValue >= distance - distanceOffset))
        {
            isMoving = false;
        }

        switch (direction)
        {
            case MovableDirection.HORIZONTAL:
                transform.position = new Vector2(startingPosition.x + pingPongValue, transform.position.y);
                break;
            case MovableDirection.VERTICAL:
                transform.position = new Vector2(transform.position.x, startingPosition.y + pingPongValue);
                break;
            case MovableDirection.DIAGONAL_UP:
                transform.position = new Vector2(startingPosition.x + pingPongValue, startingPosition.y + pingPongValue);
                break;
            case MovableDirection.DIAGONAL_DOWN:
                transform.position = new Vector2(startingPosition.x + pingPongValue, startingPosition.y - pingPongValue);
                break;
        }
    }
}

public enum MovableDirection
{
    HORIZONTAL,
    VERTICAL,
    DIAGONAL_UP,
    DIAGONAL_DOWN,
    NUM_OF_DIRECTIONS
}