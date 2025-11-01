using damage;
using death_effects;
using UnityEngine;

namespace aggregators
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TerminatesInsteadOfDying), typeof(HitPoints))]
    public class Totem : MonoBehaviour
    {
    }
}