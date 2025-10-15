using death_processors;
using UnityEngine;

namespace damage
{
    [RequireComponent(typeof(Mortal), typeof(AudioSource))]
    public class Hurtable : MonoBehaviour, IDamagableComponent
    {
        // Configurable parameters
        [SerializeField] private uint fullHealth = 100;
        [SerializeField] private uint health = 0;

        [SerializeField] private AudioClip hurtSound;

        // Other components
        private Mortal _mortal;
        private AudioSource _audioSource;

        private void Awake()
        {
            if (health == 0) health = fullHealth;
            _mortal = GetComponent<Mortal>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            CheckHealth();
        }

        public void ReceiveDamage(uint damage, AudioClip damageSound = null)
        {
            _audioSource.PlayOneShot(damageSound ?? hurtSound);
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