using UnityEngine;

namespace damage.hurting
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource), typeof(Animator))]
    public abstract class Hurtable : MonoBehaviour
    {
        [SerializeField] private AudioClip hurtSound;

        // Other components
        protected HitPoints HitPoints;
        private AudioSource _audioSource;
        private Animator _animator;

        // Summut
        private static readonly int Hurt = Animator.StringToHash("Hurt");

        protected virtual void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            Debug.Assert(_audioSource != null);
            _animator = GetComponent<Animator>();
            Debug.Assert(_animator != null);
        }

        public void ReceiveDamage(uint damage, AudioClip damageSound = null)
        {
            _audioSource.PlayOneShot(damageSound ?? hurtSound);
            HitPoints.LowerBy(damage);
            if (_animator != null)
            {
                _animator.SetTrigger(Hurt);
            }
        }
    }
}