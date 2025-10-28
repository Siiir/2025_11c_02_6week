using System.Collections.Generic;
using death_effects;
using UnityEngine;

namespace interactibles
{
    [RequireComponent(typeof(Collider2D), typeof(AudioSource))]
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] AudioClip checkPointSetSound;
        [SerializeField] AudioClip checkCannotBeSetSound;
        private Collider2D _collider2D;
        private AudioSource _audioSource;

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

            if (respawnable.respawnTransform == transform)
            {
                // Checkpoint is already set here
                _audioSource.PlayOneShot(checkCannotBeSetSound);
                return;
            }

            _audioSource.PlayOneShot(checkPointSetSound);
            respawnable.respawnTransform = transform;
        }
    }
}