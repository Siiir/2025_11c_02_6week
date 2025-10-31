using System.Text.RegularExpressions;
using damage;
using death_effects.interfaces;
using UnityEngine;

namespace death_processors
{
    [RequireComponent(typeof(AudioSource))]
    // While `Terminable` might not be directly used in this class,
    // it ensures that mortal entities are cleaned up properly when they die.
    // This can prevent resource leaks and maintain game performance.
    [RequireComponent(typeof(Terminable))]
    public class Mortal : MonoBehaviour, IDamagableComponent
    {
        // Constants
        [SerializeField] private AudioClip deathSound;
        private Collider2D _collider;
        private AudioSource _audioSource;

        // Variables
        public bool IsAlive { get; private set; } = true;
        public bool IsDead => !this.IsAlive;

        private static bool _warnedAboutAttemptToDieForSecondTime = false;

        private void Awake()
        {
            this._collider = GetComponent<Collider2D>();
            this._audioSource = this.GetComponent<AudioSource>();
        }

        public virtual void Die()
        {
            if (this.IsDead)
            {
                if (!_warnedAboutAttemptToDieForSecondTime)
                {
                    _warnedAboutAttemptToDieForSecondTime = true;
                    Debug.LogWarning(Regex.Replace($@"
                        Attempted to kill an already dead entity: {this}.
                        This warning message is shown once per simulation."
                        , @"(?m)^[ \t]+", ""));
                }

                return; // Prevent retriggering death on a dead entity.
            }

            var deathReplacement = this.GetComponent<IDeathReplacement>();
            if (deathReplacement != null)
            {
                deathReplacement.DoDeath();
            }
            else // Default death implementation
            {
                this.IsAlive = false;
                this._audioSource.PlayOneShot(this.deathSound);
                // disable collider, which will push this entity into the void
                // the void has its own ways to terminate entities
                this._collider.enabled = false;

                // here entity's controller should be disabled
                // making it unable to avoid death by falling into the void
                // this could be programmed more creatively by making this entity "Kinematic",
                // while preserving its collider or changing collision layer
            }
        }

        public void RestoreDamage()
        {
            this.IsAlive = true;
            if (this._collider != null)
            {
                this._collider.enabled = true;
            }
        }
    }
}