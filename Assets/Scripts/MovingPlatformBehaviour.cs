using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour
{
    [Header("Movement")]
    public PlatformDirection direction;
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
            case PlatformDirection.HORIZONTAL:
                transform.position = new Vector2(startingPosition.x + pingPongValue, transform.position.y);
                break;
            case PlatformDirection.VERTICAL:
                transform.position = new Vector2(transform.position.x, startingPosition.y + pingPongValue);
                break;
            case PlatformDirection.DIAGONAL_UP:
                transform.position = new Vector2(startingPosition.x + pingPongValue, startingPosition.y + pingPongValue);
                break;
            case PlatformDirection.DIAGONAL_DOWN:
                transform.position = new Vector2(startingPosition.x + pingPongValue, startingPosition.y - pingPongValue);
                break;
        }
    }
}

public enum PlatformDirection
{
    HORIZONTAL,
    VERTICAL,
    DIAGONAL_UP,
    DIAGONAL_DOWN,
    NUM_OF_DIRECTIONS
}