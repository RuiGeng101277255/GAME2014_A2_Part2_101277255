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
