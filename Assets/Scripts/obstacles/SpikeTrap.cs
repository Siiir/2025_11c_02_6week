using damage;
using damage.hurting;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private uint damageAmount = 10;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // make player bounce when stepping on spikes
            
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            
            if (rb)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                Hurtable hurtable = other.gameObject.GetComponent<Hurtable>();
                hurtable.ReceiveDamage(damageAmount);
                
            }
        }
    }

}
