using damage;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    private uint damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("SpikeTrap");
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
