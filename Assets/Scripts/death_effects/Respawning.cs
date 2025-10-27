using damage;
using death_effects.interfaces;
using death_processors;
using UnityEngine;

namespace death_effects
{
    [RequireComponent(typeof(Terminable), typeof(AudioSource))]
    public class Respawning : MonoBehaviour, ITerminationReplacement
    {
        // Fields
        [SerializeField] private Transform respawnTransform;
        [SerializeField] private AudioClip respawnSound;
        private Hurtable _hurtable;
        private AudioSource _audioSource;

        private void Awake()
        {
            this._hurtable = this.GetComponent<Hurtable>();
            this._audioSource = this.GetComponent<AudioSource>();
        }

        public void Terminate()
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