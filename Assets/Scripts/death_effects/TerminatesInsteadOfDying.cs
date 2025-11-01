using System;
using death_effects.abstracts;
using death_processors;
using UnityEngine;

namespace death_effects
{
    [RequireComponent(typeof(Terminable))]
    public class TerminatesInsteadOfDying : DeathReplacement
    {
        private Terminable _terminable;

        private void Awake()
        {
            this._terminable = this.GetComponent<Terminable>();
        }

        public override void DoDeath()
        {
            this._terminable.Terminate();
        }
    }
}