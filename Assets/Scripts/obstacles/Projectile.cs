using damage;
using death_effects;
using death_processors;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace obstacles
{
    [RequireComponent(typeof(Aging), typeof(LoseVelocityAfterDeath),
        typeof(Mortal))]
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        // Stats
        [SerializeField] private uint damageOnHit = 10;

        // Components
        private Mortal _mortal;
        private Collider2D _collider;

        private void Awake()
        {
            this._mortal = this.GetComponent<Mortal>();
            this.GetComponent<Terminable>();
            this._collider = this.GetComponent<Collider2D>();
            // Fix for current collision detection
            Debug.Assert(_collider.isTrigger, "Projectile must be triggered by the trigger collider");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var hurtable = other.gameObject.GetComponent<Hurtable>();
            if (hurtable != null)
            {
                hurtable.ReceiveDamage(damageOnHit);
            }

            this._mortal.Die();
        }
    }
}