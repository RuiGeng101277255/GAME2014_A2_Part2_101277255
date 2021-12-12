/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: SwordBehaviour.cs
 Last Modified: December 12th, 2021
 Description: Trigger for the sword's object, used to do collision detection against enemies.
 Version History: v1.02 Functional and Internal Documentation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            PlayerScript player = FindObjectOfType<PlayerScript>();
            if (player.hasJustAttacked)
            {
                player.PlayerScore += collision.gameObject.GetComponent<EnemyController>().EnemyWorth;
                Destroy(collision.gameObject);
            }
        }
    }
}
