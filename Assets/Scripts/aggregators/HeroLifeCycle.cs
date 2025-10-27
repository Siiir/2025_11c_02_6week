using damage;
using death_effects;
using UnityEngine;

namespace aggregators
{
    [RequireComponent(typeof(SceneBound), typeof(Respawning), typeof(Hurtable))]
    public class HeroLifeCycle : MonoBehaviour
    {
    }
}