using death_effects.interfaces;
using UnityEngine;

namespace death_processors
{
    public class Terminable : MonoBehaviour
    {
        public virtual void Terminate()
        {
            var terminationRepl = this.GetComponent<ITerminationReplacement>();
            if (terminationRepl == null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                terminationRepl.Terminate();
            }
        }
    }
}