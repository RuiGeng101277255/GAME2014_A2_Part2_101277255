/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: CollectableScripts.cs
 Last Modified: December 12th, 2021
 Description: Collectable Loot's Behaviours. For gems, ammo chest and health potions
 Version History: v1.04 Collision and SFX full implementation
 */

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
            FindSFX();
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

    private void FindSFX()
    {
        AudioSource LootSFX = new AudioSource();

        switch (Type)
        {
            case LootType.SMALL_GEM:
                LootSFX = GameObject.Find("CoinCollectSFX").GetComponent<AudioSource>();
                break;
            case LootType.LARGE_GEM:
                LootSFX = GameObject.Find("CoinCollectSFX").GetComponent<AudioSource>();
                break;
            case LootType.AMMO_CHEST:
                LootSFX = GameObject.Find("AmmoCollectSFX").GetComponent<AudioSource>();
                break;
            case LootType.HEALTH_POTION:
                LootSFX = GameObject.Find("HealthCollectSFX").GetComponent<AudioSource>();
                break;
        }

        LootSFX.Play();
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