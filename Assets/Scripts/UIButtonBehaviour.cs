/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: UIButtonBehaviour.cs
 Last Modified: December 12th, 2021
 Description: Detects when an onscreen button is pressed for the player to trigger jump/weapon change/attack actions.
 Version History: v1.03 Full Functionality and Internal Documentation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonBehaviour : MonoBehaviour
{
    public static bool JumpButtonDown;
    public static bool ShootButtonDown;
    public static bool ChangeWeaponDown;
    
    public void OnJumpButtonDown()
    {
        JumpButtonDown = true;
    }

    public void OnShootButtonDown()
    {
        ShootButtonDown = true;
    }

    public void OnChangeWeaponDown()
    {
        ChangeWeaponDown = true;
    }

    public void OnJumpButtonUp()
    {
        JumpButtonDown = false;
    }

    public void OnShootButtonUp()
    {
        ShootButtonDown = false;
    }

    public void OnChangeWeaponUp()
    {
        ChangeWeaponDown = false;
    }
}
