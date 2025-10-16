using System;
using death_processors;
using UnityEngine;


public class AttackArea : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;
        Mortal target = collision.GetComponent<Mortal>();
        
        if (target != null)
        {
            Debug.Log("Hit: " + collision.name);
            target.Die();
        }
        
    }
    
}
