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
        private static readonly int Die = Animator.StringToHash("Die");
        
        // Stats
        [SerializeField] private uint damageOnHit = 10;

        private bool _hasCollided;

        // Components
        private Mortal _mortal;
        private Collider2D _collider;
        private Animator _animator;
        [SerializeField] private GameObject projectileHit;

        private void Awake()
        {
            this._mortal = this.GetComponent<Mortal>();
            this._collider = this.GetComponent<Collider2D>();
            this._animator = this.GetComponent<Animator>();
            // Ensure collision detection will work.
            Debug.Assert(_collider.isTrigger, "Projectile collider must be set to trigger.");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_hasCollided) return;
            _hasCollided = true;
            
            var hurtable = other.gameObject.GetComponent<DirectlyHurtable>();
            if (hurtable != null)
            {
                hurtable.ReceiveDamage(damageOnHit);
            }

            if (_animator)
            {
                _animator.SetTrigger(Die);
            }
            
            Instantiate(projectileHit, transform.position, transform.rotation);
            
            this._mortal.Die();
        }
    }
}