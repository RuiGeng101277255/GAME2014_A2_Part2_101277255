using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScripts : MonoBehaviour
{
    public LootType Type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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