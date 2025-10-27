using damage;
using death_effects;
using UnityEngine;

namespace aggregators
{
    [RequireComponent(typeof(RespawnsAtCheckPoint), typeof(SceneBound), typeof(Hurtable))]
    public class HeroLifeCycle : MonoBehaviour
    {
    }
}