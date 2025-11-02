using death_processors;
using UnityEngine;

namespace damage
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Mortal))]
    public class HitPoints : MonoBehaviour, IDamagableComponent
    {
        // Configurable parameters
        [SerializeField] private uint fullHealth = 100;

        [SerializeField] [Tooltip("Initial Health")]
        private uint health = 0; // 0 means full health

        // Other components
        private Mortal _mortal;

        private void Awake()
        {
            if (health == 0) // sentinel value for "use full health"
            {
                health = fullHealth;
            }

            _mortal = GetComponent<Mortal>();
        }

        private void Start()
        {
            CheckHealth();
        }

        public void LowerBy(uint damage)
        {
            health = (damage >= health) ? 0 : health - damage;
            CheckHealth();
        }

        private void CheckHealth()
        {
            if (health <= 0)
            {
                this._mortal.Die();
            }
        }

        public void RestoreDamage()
        {
            this.health = fullHealth;
        }
    }
}