using damage;
using death_processors;
using UnityEngine;

namespace effects
{
    [RequireComponent(typeof(Hurtable), typeof(Mortal))]
    public class PersistentPoison : MonoBehaviour
    {
        // Configurable fields
        [SerializeField] private uint damagePerTick = 1;
        [SerializeField] private float tickInterval = 2.5f;
        [SerializeField] private AudioClip poisoningSound;

        private float _timeSinceLastTick = 0f;
        private Hurtable _hurtable;
        private Mortal _mortal;

        private void Awake()
        {
            _hurtable = GetComponent<Hurtable>();
            _mortal = GetComponent<Mortal>();
        }

        private void Update()
        {
            if (_mortal.IsAlive)
            {
                _timeSinceLastTick += Time.deltaTime;
                if (_timeSinceLastTick >= tickInterval)
                {
                    Tick();
                }
            }
            else
            {
                _timeSinceLastTick = tickInterval/2; // Reset timer to half of interval when dead
            }
        }

        private void Tick()
        {
            _hurtable.ReceiveDamage(damagePerTick, poisoningSound);
            _timeSinceLastTick = 0f;
        }
    }
}