/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: EnemyController.cs
 Last Modified: December 12th, 2021
 Description: Enemy's Behaviours based on Professor Tom's lab.
 Version History: v1.03 Minor Modification and Internal Documentations
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Player Detection")] 
    public LOS enemyLOS;

    [Header("Movement")] 
    public float runForce;
    public Transform lookAheadPoint;
    public Transform lookInFrontPoint;
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;
    public bool isGroundAhead;

    [Header("Animation")] 
    public Animator animatorController;

    [Header("Bullet Firing")] 
    public Transform bulletSpawn;
    public float fireDelay;
    public GameObject player;

    private Rigidbody2D rigidbody;

    public bool isFrog;

    public int EnemyWorth;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enemyLOS = GetComponent<LOS>();
        animatorController = GetComponent<Animator>();
        player = GameObject.FindObjectOfType<PlayerScript>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAhead();
        LookInFront();

        if (!HasLOS())
        {
            animatorController.enabled = true;
            animatorController.Play("Run");
            MoveEnemy();
        }
        else
        {
            animatorController.enabled = false;
        }
        
    }

    private bool HasLOS()
    {
        if (enemyLOS.colliderList.Count > 0)
        {
            if ((enemyLOS.collidesWith.gameObject.CompareTag("Player")) &&
                (enemyLOS.colliderList[0].gameObject.CompareTag("Player")))
            {
                return true;
            }
            else
            {
                foreach (var collider in enemyLOS.colliderList)
                {
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        var hit = Physics2D.Raycast(lookInFrontPoint.position, Vector3.Normalize(collider.transform.position - lookInFrontPoint.position), 5.0f, enemyLOS.contactFilter.layerMask);
                        
                        if((hit) && (hit.collider.gameObject.CompareTag("Player")))
                        {
                            Debug.DrawLine(lookInFrontPoint.position, collider.transform.position, Color.red);
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }


    private void LookAhead()
    {
        var hit = Physics2D.Linecast(transform.position, lookAheadPoint.position, groundLayerMask);
        isGroundAhead = (hit) ? true : false;
    }

    private void LookInFront()
    {
        var hit = Physics2D.Linecast(transform.position, lookInFrontPoint.position, wallLayerMask);
        if (hit)
        {
            Flip();
        }
    }

    private void MoveEnemy()
    {
        if (isGroundAhead)
        {
            rigidbody.AddForce(Vector2.left * runForce * transform.localScale.x);
            rigidbody.velocity *= 0.90f;

            if (isFrog)
            {
                animatorController.SetBool("isJumping", true);
            }
        }
        else
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
    }

    // EVENTS

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }

        if (collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            collision.gameObject.GetComponent<PlayerScript>().RespawnPlayer();
        }
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }

    

    // UTILITIES

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, lookAheadPoint.position);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, lookInFrontPoint.position);
    }
}
