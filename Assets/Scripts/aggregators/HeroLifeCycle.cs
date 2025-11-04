using damage.hurting;
using death_effects.respawning;
using UnityEngine;

namespace aggregators
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RespawnsAtCheckPoint), typeof(SceneBound), typeof(DirectlyHurtable))]
    public class HeroLifeCycle : MonoBehaviour
    {
    }
}