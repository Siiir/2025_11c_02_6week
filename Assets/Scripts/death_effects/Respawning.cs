using death_effects.interfaces;
using death_processors;
using UnityEngine;

namespace death_effects
{
    [RequireComponent(typeof(Terminable))]
    public class Respawning : MonoBehaviour, ITerminationReplacement
    {
        // Fields
        [SerializeField] private Transform respawnTransform;

        public void Terminate()
        {
            this.transform.position = this.respawnTransform.position;
        }
        
    }
}