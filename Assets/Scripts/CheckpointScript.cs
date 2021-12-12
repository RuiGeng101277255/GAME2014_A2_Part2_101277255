/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: CheckpointScript.cs
 Last Modified: December 12th, 2021
 Description: Set's player's spawnpoint when a specific checkpoint has been triggered.
 Version History: v1.02 Functional and added internal documentation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public Transform SpawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerScript>() != null)
        {
            collision.GetComponent<PlayerScript>().spawnPoint = SpawnPoint.position;
        }
    }
}
