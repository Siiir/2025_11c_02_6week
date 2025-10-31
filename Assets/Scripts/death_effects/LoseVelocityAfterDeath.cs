using System;
using death_effects.abstracts;
using UnityEngine;

namespace death_effects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class LoseVelocityAfterDeath : PostDeath
    {
        private Rigidbody2D _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        public override void DoPostDeath()
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }
    }
}