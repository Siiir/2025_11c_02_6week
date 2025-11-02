using damage;
using death_effects.interfaces;
using death_processors;
using UnityEngine;

namespace death_effects.respawning
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Terminable), typeof(AudioSource))]
    public abstract class Respawning : MonoBehaviour, ITerminationReplacement
    {
        // Fields
        [SerializeField] public Transform respawnTransform;
        [SerializeField] private AudioClip respawnSound;
        private AudioSource _audioSource;

        private void Awake()
        {
            this._audioSource = this.GetComponent<AudioSource>();
        }

        public virtual void Terminate()
        {
            this._audioSource.PlayOneShot(this.respawnSound);
            foreach (var component in GetComponents<IDamagableComponent>())
            {
                component.RestoreDamage();
            }

            this.transform.position = this.respawnTransform.position;
        }
    }
}