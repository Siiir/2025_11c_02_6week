using System;
using death_processors;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private GameObject owner;
    
    public void SetOwner(GameObject ownerObj)
    {
        owner = ownerObj;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger detected: " + collision.name + ", Active: " + gameObject.activeSelf);
        
        
        Mortal target = collision.GetComponent<Mortal>();

        Debug.Log(collision.gameObject.name + " got hit!");
        if (target != null)
        {
            Debug.Log("Hit: " + collision.name);
            target.Die();
        }
        if (collision.gameObject == owner) return;
    }
    
    // debug below
    private void OnDrawGizmos()
    {
        var collider = GetComponent<BoxCollider2D>();
        if (collider == null) return;

        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(collider.offset, collider.size);
    }
}
