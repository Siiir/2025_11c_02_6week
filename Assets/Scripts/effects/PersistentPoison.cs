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

        // Constants
        private float _timeBeforeFirstTick;
        private Hurtable _hurtable;

        private Mortal _mortal;

        // Variables
        private float _timeTillNextTick;

        private void Awake()
        {
            _timeBeforeFirstTick = tickInterval / 2;
            _timeTillNextTick = _timeBeforeFirstTick;
            _hurtable = GetComponent<Hurtable>();
            _mortal = GetComponent<Mortal>();
        }

        private void Update()
        {
            if (_mortal.IsAlive)
            {
                _timeTillNextTick -= Time.deltaTime;
                if (_timeTillNextTick <= 0)
                {
                    _timeTillNextTick += tickInterval;
                    Tick();
                }
            }
            else
            {
                _timeTillNextTick = _timeBeforeFirstTick;
            }
        }

        private void Tick()
        {
            _hurtable.ReceiveDamage(damagePerTick, poisoningSound);
        }
    }
}