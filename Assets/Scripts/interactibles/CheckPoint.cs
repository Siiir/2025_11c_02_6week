using System.Collections.Generic;
using death_effects.respawning;
using UnityEngine;

namespace interactibles
{
    [RequireComponent(typeof(Collider2D), typeof(AudioSource))]
    public class CheckPoint : MonoBehaviour
    {
        // Stats & Tweaks
        [SerializeField] private bool playCpCannotBeSetSound;

        // Serialized Fields
        [SerializeField] private AudioClip checkPointSetSound;

        [SerializeField] private AudioClip cpCannotBeSetSound;

        // Components
        private Collider2D _collider2D;

        private AudioSource _audioSource;

        // Other Fields
        private readonly HashSet<GameObject> _suppressNextCheckIn = new HashSet<GameObject>();

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _audioSource = GetComponent<AudioSource>();

            // Ensure this collider is used as a trigger
            if (_collider2D != null)
            {
                if (!_collider2D.isTrigger)
                {
                    _collider2D.isTrigger = true;
                    Debug.LogWarning(
                        "`Collider2D` had been automatically set to `isTrigger` for CheckPoint functionality.");
                }
            }
        }

        public void SuppressNextCheckInFor(GameObject obj)
        {
            _suppressNextCheckIn.Add(obj);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var respawnable = other.GetComponent<RespawnsAtCheckPoint>();
            if (respawnable != null)
            {
                AttemptCheckIn(respawnable);
            }
        }

        private void AttemptCheckIn(RespawnsAtCheckPoint respawnable)
        {
            if (_suppressNextCheckIn.Contains(respawnable.gameObject))
            {
                _suppressNextCheckIn.Remove(respawnable.gameObject);
                return;
            }

            // If the checkpoint is already set here
            if (respawnable.respawnTransform == this.transform)
            {
                if (playCpCannotBeSetSound)
                {
                    _audioSource.PlayOneShot(cpCannotBeSetSound);
                }

                return;
            }

            // Set the checkpoint
            _audioSource.PlayOneShot(checkPointSetSound);
            respawnable.respawnTransform = transform;
        }
    }
}