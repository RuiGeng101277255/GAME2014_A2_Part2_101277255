/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: BackgroundScrolling.cs
 Last Modified: November 21st, 2021
 Description: Scrolling the scroll of cloud background
 Version History: v1.01 Initial blank script and internal documentation
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
