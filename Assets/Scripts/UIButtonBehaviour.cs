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
        Debug.Log("JumpButtonDown = " + JumpButtonDown);
        JumpButtonDown = true;
    }

    public void OnShootButtonDown()
    {
        Debug.Log("ShootButtonDown = " + ShootButtonDown);
        ShootButtonDown = true;
    }

    public void OnChangeWeaponDown()
    {
        Debug.Log("ChangeWeaponDown = " + ChangeWeaponDown);
        ChangeWeaponDown = true;
    }

    public void OnJumpButtonUp()
    {
        Debug.Log("JumpButtonDown = " + JumpButtonDown);
        JumpButtonDown = false;
    }

    public void OnShootButtonUp()
    {
        Debug.Log("ShootButtonDown = " + ShootButtonDown);
        ShootButtonDown = false;
    }

    public void OnChangeWeaponUp()
    {
        Debug.Log("ChangeWeaponDown = " + ChangeWeaponDown);
        ChangeWeaponDown = false;
    }
}
