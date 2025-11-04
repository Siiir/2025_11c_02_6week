using damage;
using damage.hurting;
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
            this._collider = this.GetComponent<Collider2D>();
            // Ensure collision detection will work.
            Debug.Assert(_collider.isTrigger, "Projectile collider must be set to trigger.");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var hurtable = other.gameObject.GetComponent<DirectlyHurtable>();
            if (hurtable != null)
            {
                hurtable.ReceiveDamage(damageOnHit);
            }

            this._mortal.Die();
        }
    }
}