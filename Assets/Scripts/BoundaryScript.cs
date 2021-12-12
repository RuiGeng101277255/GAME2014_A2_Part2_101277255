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
            Destroy(collision.gameObject);
        }
    }
}
