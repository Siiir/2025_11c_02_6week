using System;
using death_processors;
using UnityEngine;

namespace death_effects
{
    [RequireComponent(typeof(Terminable))]
    public class TerminatesInVoid : MonoBehaviour
    {
        // Fields

        private Terminable _terminable;

        // Methods

        private void Awake()
        {
            this._terminable = this.GetComponent<Terminable>();
        }

        private void Update()
        {
            if (Single.IsNaN(BottomWorldBorder.Y))
            {
                throw new Exception("BottomWorldBorder.Y is not set");
            }
            if (this.transform.position.y < BottomWorldBorder.Y)
            {
                this._terminable.Terminate();
            }
        }
    }
}