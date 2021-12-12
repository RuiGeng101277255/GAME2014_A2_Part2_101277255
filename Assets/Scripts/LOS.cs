/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: LOS.cs
 Last Modified: December 12th, 2021
 Description: Line of Sight Behaviours based on Professor Tom's lab.
 Version History: v1.03 Minor Modification and Internal Documentations
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[System.Serializable]
public class LOS : MonoBehaviour
{
    [Header("Detection Properties")] 
    public Collider2D collidesWith; // debug
    public ContactFilter2D contactFilter;
    public List<Collider2D> colliderList;

    private PolygonCollider2D LOSCollider;

    // Start is called before the first frame update
    void Start()
    {
        LOSCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Physics2D.GetContacts(LOSCollider, contactFilter, colliderList);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Bullet"))
        {
            collidesWith = other;
        }
    }
}
