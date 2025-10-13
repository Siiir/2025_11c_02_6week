using System;
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
        private AudioSource _audioSource;

        private void Awake()
        {
            this._audioSource = this.GetComponent<AudioSource>();
        }

        public void Terminate()
        {
            this._audioSource.PlayOneShot(this.respawnSound);
            this.transform.position = this.respawnTransform.position;
        }
        
    }
}