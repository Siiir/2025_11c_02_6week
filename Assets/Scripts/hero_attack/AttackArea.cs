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
        if (collision.gameObject == owner) return;
        Mortal target = collision.GetComponent<Mortal>();
        
        if (target != null)
        {
            Debug.Log("Hit: " + collision.name);
            target.Die();
        }
        
    }
    
    // debug
    private void OnDrawGizmos()
    {
        var collider = GetComponent<BoxCollider2D>();
        if (collider == null) return;

        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(collider.offset, collider.size);
    }
}
