using Player;
using UnityEngine;

namespace aggregators
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BasicPlayerMovement), typeof(Physical))]
    public class HeroMovement : MonoBehaviour
    {
    }
}