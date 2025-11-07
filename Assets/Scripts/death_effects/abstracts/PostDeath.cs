using death_effects.interfaces;
using death_processors;
using UnityEngine;

namespace death_effects.abstracts
{
    [RequireComponent(typeof(AgonyfulMortal))]
    public abstract class PostDeath : MonoBehaviour, IPostDeath
    {
        public abstract void DoPostDeath();
    }
}