using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScripts : MonoBehaviour
{
    public LootType Type;
    public int ScoreValue;
    public int AmmoCount;
    public bool isHealthPotion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            collision.gameObject.GetComponent<PlayerScript>().PlayerScore += ScoreValue;
            collision.gameObject.GetComponent<PlayerScript>().AmmmoCount += AmmoCount;

            if (isHealthPotion)
            {
                if (collision.gameObject.GetComponent<PlayerScript>().PlayerLive < 4)
                {
                    collision.gameObject.GetComponent<PlayerScript>().PlayerLive++;
                }
            }

            gameObject.SetActive(false);
        }
    }
}

public enum LootType
{
    SMALL_GEM,
    LARGE_GEM,
    AMMO_CHEST,
    HEALTH_POTION,
    NUM_OF_LOOT
}