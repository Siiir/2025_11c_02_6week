using death_effects.interfaces;
using death_processors;
using UnityEngine;

namespace death_effects.abstracts
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Mortal))]
    public abstract class DeathReplacement : MonoBehaviour, IDeathReplacement
    {
        public abstract void DoDeath();
    }
}