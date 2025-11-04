using UnityEngine;

namespace aggregators
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(HeroLifeCycle), typeof(HeroMovement), typeof(HeroAttack))]
    public class Hero : MonoBehaviour
    {
    }
}