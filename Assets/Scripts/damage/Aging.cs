using death_processors;
using UnityEngine;

namespace damage
{
    [RequireComponent(typeof(Mortal))]
    public class Aging : MonoBehaviour, IDamagableComponent
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

        public void RestoreDamage()
        {
            currentAgeInSecs = 0.0f;
        }
    }
}