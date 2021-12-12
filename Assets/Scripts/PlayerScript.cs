/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: PlayerScript.cs
 Last Modified: November 21st, 2021
 Description: Defines the player's behaviour
 Version History: v1.01 Initial blank script and internal documentation
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

    [Header("Attack Animation")]
    public GameObject SwordObject;
    public GameObject RifleObject;
    public bool isSword;
    bool hasJustChangedWeapon = false;
    bool hasJustAttacked = false;

    [Header("SFX")]
    public AudioSource jumpSFX;

    [Header("Spawnpoint")]
    public Vector2 spawnPoint;

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

            // jump activated
            if (jump > 0)
            {
                //jumpSFX.Play();
            }

            // Check for Flip
            

            if (x != 0)
            {
                playerAnim.SetInteger("Movement", (int)PlayerMovementAnimation.RUN);
                //x = FlipAnimation(x);
                //animatorController.SetInteger("AnimationState", (int)PlayerAnimationState.RUN); // RUN State
                //state = PlayerAnimationState.RUN;
                //CreateDustTrail();
            }
            else
            {
                playerAnim.SetInteger("Movement", (int)PlayerMovementAnimation.IDLE);
                //animatorController.SetInteger("AnimationState", (int)PlayerAnimationState.IDLE); // IDLE State
                //state = PlayerAnimationState.IDLE;
            }

            float horizontalMoveForce = x * horizontalForce;
            float jumpMoveForce = jump * verticalForce;

            float mass = playerRB.mass * playerRB.gravityScale;


            playerRB.AddForce(new Vector2(horizontalMoveForce, jumpMoveForce) * mass);
            playerRB.velocity *= 0.9f; // scaling / stopping hack
        }
        else // Air Control
        {
            //animatorController.SetInteger("AnimationState", (int)PlayerAnimationState.JUMP); // JUMP State
            //state = PlayerAnimationState.JUMP;

            playerAnim.SetInteger("Movement", (int)PlayerMovementAnimation.JUMP);

            if (x != 0)
            {
                //x = FlipAnimation(x);
                //
                float horizontalMoveForce = x * horizontalForce * airControlFactor;
                float mass = playerRB.mass * playerRB.gravityScale;
                //
                playerRB.AddForce(new Vector2(horizontalMoveForce, 0.0f) * mass);
            }
        }
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundLevel.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);

        grounded = (hit) ? true : false;
    }

    private void Attack()
    {


        if ((UIButtonBehaviour.ShootButtonDown) || (Input.GetKeyDown(KeyCode.J)))
        {
            //Debug.Log(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Movement"));

            if (!hasJustAttacked)
            {
                if(isSword)
                {
                    Debug.Log("Attack");
                    //SwordObject.SetActive(true);
                    //playerAnim.SetTrigger("SwordAttack");
                    playerAnim.SetInteger("Movement", (int)PlayerMovementAnimation.SWORD);
                }
                else
                {

                }

                hasJustAttacked = true;
            }
        }

        if (hasJustAttacked)
        {
            if (isSword)
            {
                if (playerAnim.GetCurrentAnimatorStateInfo(3).normalizedTime > 1.0f)
                {
                    //playerAnim.SetInteger("Action", (int)PlayerAttackAction.NONE);
                    //playerAnim.SetInteger("Movement", (int)PlayerMovementAnimation.NONE);
                    SwordObject.SetActive(false);
                    hasJustAttacked = false;
                }
            }
        }

        //else
        //{
        //    playerAnim.SetInteger("Action", (int)PlayerAttackAction.NONE);
        //    SwordObject.SetActive(false);
        //    hasJustAttacked = false;
        //}
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