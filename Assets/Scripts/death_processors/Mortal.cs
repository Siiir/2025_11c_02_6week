using death_effects.interfaces;
using UnityEngine;

namespace death_processors
{
    public class Mortal : MonoBehaviour
    {
        public virtual void Die()
        {
            var deathReplacement = this.GetComponent<IDeathReplacement>();
            if (deathReplacement == null)
            {
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
