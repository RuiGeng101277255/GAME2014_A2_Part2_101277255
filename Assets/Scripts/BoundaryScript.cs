/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: BoundaryScript.cs
 Last Modified: December 12th, 2021
 Description: Boundary collision that will reset the player and the falling platforms.
 Version History: v1.03 Functional and added internal documentation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            collision.gameObject.GetComponent<PlayerScript>().RespawnPlayer();
        }
        else
        {
            if (collision.gameObject.GetComponent<SpecialPlatformBehaviour>() != null)
            {
                collision.gameObject.GetComponent<SpecialPlatformBehaviour>().resetPlatform();
            }
        }
    }
}
