using damage;
using UnityEngine;

namespace aggregators
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Physical : MonoBehaviour, IDamagableComponent
    {
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void RestoreDamage()
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
    }
}