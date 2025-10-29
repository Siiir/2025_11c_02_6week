using death_effects.interfaces;
using death_processors;
using UnityEngine;

namespace damage
{
    [RequireComponent(typeof(AgonyfulMortal))]
    public class Aging : MonoBehaviour, IDamagableComponent, IPostDeath
    {
        [SerializeField] private float maxAgeInSecs = 6.0f;

        [Tooltip("Initial age in seconds")] [SerializeField]
        private float currentAgeInSecs = 0.0f;

        private Mortal _mortal;

        private void Awake()
        {
            _mortal = GetComponent<Mortal>();
        }

        private void FixedUpdate()
        {
            currentAgeInSecs += Time.deltaTime;
            if (currentAgeInSecs >= maxAgeInSecs)
            {
                _mortal.Die();
            }
        }

        public void DoPostDeath()
        {
            this.enabled = false;
        }
        
        public void RestoreDamage()
        {
            currentAgeInSecs = 0.0f;
            this.enabled = true;
        }
    }
}