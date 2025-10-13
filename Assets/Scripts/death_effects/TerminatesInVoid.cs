using System;
using death_processors;
using UnityEngine;

namespace death_effects
{
    [RequireComponent(typeof(Terminable))]
    public class TerminatesInVoid : MonoBehaviour
    {
    
        // Fields
    
        private const String BottomDeathLineName = "BottomDeathLine";
    
        private static readonly Lazy<float> TerminateWhenBelow = new Lazy<float>(() =>
        {
            GameObject bottomDeathLine = GameObject.Find(BottomDeathLineName);
            if (bottomDeathLine == null)
            {
                throw new Exception($"{BottomDeathLineName} not found in the simulation!");
            }
            return bottomDeathLine.transform.position.y;
        });
    
        private Terminable _terminable;

        // Methods
    
        private void Awake()
        {
            this._terminable = this.GetComponent<Terminable>();
        }

        private void Update()
        {
            if (this.transform.position.y < TerminateWhenBelow.Value)
            {
                this._terminable.Terminate();
            }
        }
    }
}