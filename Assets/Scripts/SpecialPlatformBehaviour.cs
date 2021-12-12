using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlatformBehaviour : MonoBehaviour
{
    public SpecialPlatformType PlatformType;
    public float CountDownTimer;

    Rigidbody2D platformRB;

    bool hasPlayerLanded = false;

    void Start()
    {
        platformRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlatformStatus();
    }

    void CheckPlatformStatus()
    {
        if (hasPlayerLanded)
        {
            if (CountDownTimer > 0.0f)
            {
                CountDownTimer -= Time.deltaTime;
            }
            else
            {
                doPlatformAction();
            }
        }
    }

    void doPlatformAction()
    {
        switch (PlatformType)
        {
            case SpecialPlatformType.FALLING:
                platformRB.constraints = RigidbodyConstraints2D.None;
                break;
            case SpecialPlatformType.EXPLODING:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            hasPlayerLanded = true;
        }
    }
}

public enum SpecialPlatformType
{
    FALLING,
    EXPLODING
}