using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlatformBehaviour : MonoBehaviour
{
    public SpecialPlatformType PlatformType;
    public float CountDownTimer;

    Rigidbody2D platformRB;
    Animator platformAnim;

    bool hasPlayerLanded = false;
    bool isExploding = false;

    void Start()
    {
        platformRB = GetComponent<Rigidbody2D>();

        if (PlatformType == SpecialPlatformType.EXPLODING)
        {
            platformAnim = GetComponent<Animator>();
        }
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
                if (!isExploding)
                {
                    platformAnim.SetTrigger("Explode");
                    isExploding = true;
                }
                else
                {
                    if ((platformAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f) && (platformAnim.GetCurrentAnimatorStateInfo(0).IsName("ExplosionAnimation")))
                    {
                        Destroy(gameObject);
                    }
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            hasPlayerLanded = true;

            if (isExploding)
            {
                collision.gameObject.GetComponent<PlayerScript>().RespawnPlayer();
            }
        }
    }
}

public enum SpecialPlatformType
{
    FALLING,
    EXPLODING
}