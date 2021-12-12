using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlatformBehaviour : MonoBehaviour
{
    public SpecialPlatformType PlatformType;
    public float CountDownTimer;

    Rigidbody2D platformRB;
    Animator platformAnim;

    Vector2 spawnPosition;

    bool hasPlayerLanded = false;
    bool isExploding = false;

    void Start()
    {
        platformRB = GetComponent<Rigidbody2D>();
        spawnPosition = platformRB.position;

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
                    GameObject.Find("PlatformExplosionSFX").GetComponent<AudioSource>().Play();
                    platformAnim.SetTrigger("Explode");
                    isExploding = true;
                }
                else
                {
                    if ((platformAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f) && (platformAnim.GetCurrentAnimatorStateInfo(0).IsName("ExplosionAnimation")))
                    {
                        gameObject.SetActive(false);
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

    public void resetPlatform()
    {
        platformRB.position = spawnPosition;
        platformRB.constraints = RigidbodyConstraints2D.FreezeAll;
        hasPlayerLanded = false;
        isExploding = false;
        gameObject.SetActive(false);
    }
}

public enum SpecialPlatformType
{
    FALLING,
    EXPLODING
}