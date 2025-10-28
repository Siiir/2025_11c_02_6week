using death_processors;
using UnityEngine;

namespace damage
{
    [RequireComponent(typeof(Mortal), typeof(AudioSource))]
    public class Hurtable : MonoBehaviour, IDamagableComponent
    {
        // Configurable parameters
        [SerializeField] private uint fullHealth = 100;

        [SerializeField] [Tooltip("Initial Health")]
        private uint health = 0; // 0 means full health

        [SerializeField] private AudioClip hurtSound;

        // Other components
        private Mortal _mortal;
        private AudioSource _audioSource;
        private Animator _animator;
        private static readonly int Hurt = Animator.StringToHash("Hurt");

        private void Awake()
        {
            if (health == 0) // sentinel value for "use full health"
            {
                health = fullHealth;
            }

            _mortal = GetComponent<Mortal>();
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
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
            if (_animator != null)
            {
                _animator.SetTrigger(Hurt);
            }
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