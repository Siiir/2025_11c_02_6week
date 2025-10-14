using death_effects.interfaces;
using UnityEngine;

namespace death_processors
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Terminable))]
    public class Mortal : MonoBehaviour
    {
        [SerializeField] private AudioClip deathSound;
        private AudioSource _audioSource;

        private void Awake()
        {
            this._audioSource = this.GetComponent<AudioSource>();
        }
        public virtual void Die()
        {
            var deathReplacement = this.GetComponent<IDeathReplacement>();
            if (deathReplacement == null)
            {
                this._audioSource.PlayOneShot(this.deathSound);
                // disable collider, which will push this entity into the void
                // the void has its own ways to terminate entities
                var collider = this.GetComponent<Collider>();
                if (collider != null)
                {
                    collider.enabled = false;
                }
                // here entity's controller should be disabled
                // making it unable to avoid death by falling into the void
                // this could be programmed more creatively by making this entity "Kinematic",
                // while preserving its collider or changing collision layer
            }
            else
            {
                deathReplacement.DoDeath();
            }
        }
    }
}
