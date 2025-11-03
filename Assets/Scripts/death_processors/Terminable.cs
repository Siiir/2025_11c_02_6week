using death_effects.interfaces;
using UnityEngine;

namespace death_processors
{
    [DisallowMultipleComponent]
    public class Terminable : MonoBehaviour
    {
        public void Terminate()
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