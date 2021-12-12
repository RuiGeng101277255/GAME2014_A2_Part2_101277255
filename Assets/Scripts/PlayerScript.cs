/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: PlayerScript.cs
 Last Modified: December 12th, 2021
 Description: Defines the player's behaviour
 Version History: v1.15 Full Player Functionality (Attack, Animation, Movement, Collision) and SFX Implementation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Input")]
    public Joystick joystick;
    public float sensitivity;

    [Header("Player Info")]
    public int AmmmoCount;
    public int PlayerScore;
    public int PlayerLive;

    [Header("Movement & Grounding")]
    Rigidbody2D playerRB;
    Animator playerAnim;
    SpriteRenderer playerSprite;
    public static bool jumpButtonDown;
    public float horizontalForce;
    public float verticalForce;
    public float airControlFactor;

    public bool grounded;
    public Transform groundLevel;
    public float groundRadius;
    public LayerMask groundLayerMask;

    public bool isMoving;

    [Header("Attack Animation")]
    public GameObject SwordObject;
    public GameObject SwordWeaponObject;
    public GameObject RifleObject;
    public bool isSword;
    bool hasJustChangedWeapon = false;
    public bool hasJustAttacked = false;

    [Header("SFX")]
    public AudioSource jumpSFX;
    public AudioSource shootSFX;
    public AudioSource swordSFX;
    bool jumpSFXPlayed = true;

    [Header("Spawnpoint")]
    public Vector2 spawnPoint;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();

        spawnPoint = playerRB.position;

        checkWeaponDisplay();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckIfGrounded();
        checkChangeWeapon();
        Attack();
        CheckIfGameOver();
    }

    private void Move()
    {
        float x = (Input.GetAxisRaw("Horizontal") + joystick.Horizontal) * sensitivity;

        playerSprite.flipX = (x >= 0) ? true : false;

        int scale = (x >= 0) ? 1 : -1;

        if (isSword)
        {
            SwordObject.transform.localScale = new Vector3(scale, 1, 1);
        }
        else
        {
            RifleObject.transform.localScale = new Vector3(scale, 1, 1);
        }

        if (grounded)
        {
            // Keyboard Input
            float y = (Input.GetAxisRaw("Vertical") + joystick.Vertical) * sensitivity;
            float jump = Input.GetAxisRaw("Jump") + ((UIButtonBehaviour.JumpButtonDown) ? 1.0f : 0.0f);

            if (x != 0)
            {
                playerAnim.SetInteger("Movement", (int)PlayerMovementAnimation.RUN);
                isMoving = true;
            }
            else
            {
                playerAnim.SetInteger("Movement", (int)PlayerMovementAnimation.IDLE);
                isMoving = false;
            }

            float horizontalMoveForce = x * horizontalForce;
            float jumpMoveForce = jump * verticalForce;

            float mass = playerRB.mass * playerRB.gravityScale;


            playerRB.AddForce(new Vector2(horizontalMoveForce, jumpMoveForce) * mass);
            playerRB.velocity *= 0.9f;
        }
        else
        {

            playerAnim.SetInteger("Movement", (int)PlayerMovementAnimation.JUMP);
            if (!jumpSFX.isPlaying)
            {
                if (!jumpSFXPlayed)
                {
                    jumpSFX.Play();
                    jumpSFXPlayed = true;
                }
            }

            if (x != 0)
            {
                float horizontalMoveForce = x * horizontalForce * airControlFactor;
                float mass = playerRB.mass * playerRB.gravityScale;
                isMoving = true;
                playerRB.AddForce(new Vector2(horizontalMoveForce, 0.0f) * mass);
            }
        }
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundLevel.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);

        grounded = (hit) ? true : false;

        if (grounded)
        {
            jumpSFXPlayed = false;
        }
    }

    private void Attack()
    {
        if ((UIButtonBehaviour.ShootButtonDown) || (Input.GetKeyDown(KeyCode.J)))
        {

            if (!hasJustAttacked)
            {
                if(isSword)
                {
                    Debug.Log("Attack");
                    SwordObject.SetActive(true);
                    SwordObject.GetComponent<Animator>().SetTrigger("swing");
                    SwordWeaponObject.GetComponent<Animator>().SetTrigger("swing");
                    playerAnim.SetInteger("Movement", (int)PlayerMovementAnimation.SWORD);
                    swordSFX.Play();
                }
                else
                {
                    fireBullet();
                    shootSFX.Play();
                }

                hasJustAttacked = true;
            }
        }

        if (hasJustAttacked)
        {
            if (!UIButtonBehaviour.ShootButtonDown)
            {
                hasJustAttacked = false;
            }
        }
    }

    private void CheckIfGameOver()
    {
        if (PlayerLive <= 0)
        {
            GameOverResult.Instance().setResultStats(PlayerScore, false);
            ScenePanelChange.Instance().openScene("EndScene");
        }
    }

    private void fireBullet()
    {
        if (AmmmoCount > 0)
        {
            var temp_bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity);
            temp_bullet.GetComponent<BulletController>().direction = Vector3.Normalize(new Vector3(transform.localScale.x, 0.0f, 0.0f));
            AmmmoCount--;
        }
    }

    private void checkChangeWeapon()
    {
        if (UIButtonBehaviour.ChangeWeaponDown)
        {
            if (!hasJustChangedWeapon)
            {
                Debug.Log("weapon changed");
                isSword = (isSword) ? false : true;
                checkWeaponDisplay();
                hasJustChangedWeapon = true;
            }
        }
        else
        {
            hasJustChangedWeapon = false;
        }
    }

    private void checkWeaponDisplay()
    {
        if (isSword)
        {
            SwordObject.SetActive(true);
            RifleObject.SetActive(false);
        }
        else
        {
            RifleObject.SetActive(true);
            SwordObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundLevel.position, groundRadius);
    }

    public void RespawnPlayer()
    {
        PlayerLive--;
        playerRB.position = spawnPoint;

        SpecialPlatformBehaviour[] allSpecialPlatforms = FindObjectsOfType<SpecialPlatformBehaviour>();

        foreach(SpecialPlatformBehaviour sp in allSpecialPlatforms)
        {
            sp.gameObject.SetActive(true);
        }
    }
}

public enum PlayerMovementAnimation
{
    IDLE,
    RUN,
    JUMP,
    SWORD
}