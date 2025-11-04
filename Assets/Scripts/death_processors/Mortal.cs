using System;
using System.Text.RegularExpressions;
using damage;
using death_effects;
using death_effects.interfaces;
using UnityEngine;

namespace death_processors
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    // While technically `TerminatesInVoid` might not be necessary for all mortal entities,
    // it ensures that mortal entities are cleaned up properly when they die.
    // This can prevent resource leaks and maintain game performance.
    [RequireComponent(typeof(TerminatesInVoid))]
    public class Mortal : MonoBehaviour, IDamagableComponent
    {
        // Constants
        [SerializeField] private AudioClip deathSound;

        // Fetched Components - Obligatory
        private AudioSource _audioSource;

        // Fetched Components - Optional
        private Transform _parentTransform;
        private Rigidbody2D _rigidbody;
        private Collider2D[] _colliders;

        // Variables
        public bool IsAlive { get; private set; } = true;
        public bool IsDead => !this.IsAlive;

        private static bool _warnedAboutAttemptToDieForSecondTime = false;

        private void Awake()
        {
            // Obligatory components
            this._parentTransform = this.transform.parent;
            this._audioSource = this.GetComponent<AudioSource>();
            // Optional components
            this._rigidbody = this.GetComponent<Rigidbody2D>();
            this._colliders = GetComponentsInChildren<Collider2D>();
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
                // disable colliders, which will push this entity into the void.
                // The void has its own ways to terminate entities
                Array.ForEach(_colliders, c => c.enabled = false);
                if (_rigidbody.bodyType == RigidbodyType2D.Static)
                {
                    _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                }

                transform.parent = null;
                // here entity's controller should be disabled,
                // making it unable to avoid death by falling into the void
                // this could be programmed more creatively by making this entity "Kinematic",
                // while preserving its collider or changing collision layer
            }
        }

        public void RestoreDamage()
        {
            this.IsAlive = true;
            Array.ForEach(_colliders, c => c.enabled = true);
            transform.parent = _parentTransform;
        }
    }
}