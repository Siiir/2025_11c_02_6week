using death_effects.interfaces;
using death_processors;
using UnityEngine;

namespace death_effects.abstracts
{
    [RequireComponent(typeof(AgonyfulMortal))]
    public abstract class PreDeath : MonoBehaviour, IPreDeath
    {
        public abstract void DoPreDeath();
    }
}