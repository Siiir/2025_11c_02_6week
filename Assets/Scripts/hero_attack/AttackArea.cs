using damage;
using death_processors;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    
    private uint damage;

    public void SetDamage(uint dmg)
    {
        damage = dmg;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;
        Hurtable target = collision.GetComponent<Hurtable>();
        
        if (target != null)
        {
            Debug.Log("Hit: " + collision.name);
            target.ReceiveDamage(damage);
        }
        
    }
    
}
