/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: BackgroundScrolling.cs
 Last Modified: December 12th, 2021
 Description: Cloud Background moves based on the player's position
 Version History: v1.02 Altered the position of the background transform
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public PlayerScript Player;

    // Update is called once per frame
    void Update()
    {
        setBackgroundPosition();
    }

    void setBackgroundPosition()
    {
        if (Player != null)
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 2, transform.position.z);
        }
    }
}
