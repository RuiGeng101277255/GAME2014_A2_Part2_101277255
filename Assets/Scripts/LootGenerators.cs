/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: LootGenerators.cs
 Last Modified: December 12th, 2021
 Description: Random Loot Generator Based On Given Spawn Positions
 Version History: v1.03 Random Functionality Implemented and Internal Documentation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGenerators : MonoBehaviour
{
    public Transform[] SpawnPoints;

    public GameObject SmallGemPrefab;
    public GameObject LargeGemPrefab;
    public GameObject AmmoChestPrefab;
    public GameObject HealthPotionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        spawnLootRandomly();
    }

    void spawnLootRandomly()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            GameObject newGameObject = new GameObject();
            LootType randType = (LootType)Random.Range(0, 4);

            switch (randType)
            {
                case LootType.SMALL_GEM:
                    newGameObject = Instantiate(SmallGemPrefab);
                    break;
                case LootType.LARGE_GEM:
                    newGameObject = Instantiate(LargeGemPrefab);

                    break;
                case LootType.AMMO_CHEST:
                    newGameObject = Instantiate(AmmoChestPrefab);
                    break;
                case LootType.HEALTH_POTION:
                    newGameObject = Instantiate(HealthPotionPrefab);
                    break;
            }
            newGameObject.transform.position = SpawnPoints[i].position;
            newGameObject.transform.SetParent(transform);
            newGameObject.SetActive(true);
        }
    }
}
