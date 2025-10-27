using damage;
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
            target.ReceiveDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        // Try to get the collider
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null) return;

        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;

        // Draw a wireframe cube if it's a BoxCollider2D
        if (collider is BoxCollider2D box)
        {
            Gizmos.DrawWireCube(box.offset, box.size);
        }
        // Draw a wireframe circle if it's a CircleCollider2D
        else if (collider is CircleCollider2D circle)
        {
            Gizmos.DrawWireSphere(circle.offset, circle.radius);
        }
    }
}