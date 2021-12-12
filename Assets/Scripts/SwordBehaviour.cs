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
