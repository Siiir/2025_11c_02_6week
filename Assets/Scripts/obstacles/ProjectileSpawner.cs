using System.Collections;
using UnityEngine;

namespace obstacles
{
    [RequireComponent(typeof(AudioSource))]
    public class ProjectileSpawner : MonoBehaviour
    {
        // References
        [SerializeField] private Transform heroTransform;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Animator bodyAnimator;
        private static readonly int Attack = Animator.StringToHash("Attack");


        [SerializeField] private AudioClip shotSound;

        // Constants
        [SerializeField] private bool disableRendering = true;
        [SerializeField] private float initMinDispenseInterval = 0.5f;
        [SerializeField] private float minDispenseInterval = 2.0f;
        [SerializeField] private float maxDispenseInterval = 2.4f;

        // Variables
        [Tooltip("Initial Dispense Cooldown (s). `NaN` to randomize on start.")] [SerializeField]
        private float dispenseCooldown = float.NaN;

        // Components
        private AudioSource _audioSource;

        private void Awake()
        {
            if (float.IsNaN(dispenseCooldown))
            {
                InitDispenseCooldown();
            }

            // Components
            _audioSource = GetComponent<AudioSource>();
            if (disableRendering)
            {
                var renderers = GetComponentsInChildren<Renderer>();
                foreach (var r in renderers)
                {
                    if (r != null)
                        r.enabled = false;
                }
            }
        }

        private void FixedUpdate()
        {
            dispenseCooldown -= Time.deltaTime;

            if (dispenseCooldown <= 0)
            {
                // dispense projectile when hero is near
                float distance = Mathf.Sqrt(
                    Mathf.Pow(heroTransform.position.x - this.GetComponent<Transform>().position.x, 2)
                    + Mathf.Pow(heroTransform.position.y - this.GetComponent<Transform>().position.y, 2));
                if (distance < 30)
                {
                    StartCoroutine(DispenseProjectileWithDelay());
                    ResetDispenseCooldown();
                }
            }
        }

        private IEnumerator DispenseProjectileWithDelay()
        {
            if (bodyAnimator)
                bodyAnimator.SetTrigger(Attack);

            yield return new WaitForSeconds(0.3f);

            _audioSource.PlayOneShot(shotSound);
            var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            // Apply initial force/velocity
            var rb = projectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = (Vector2)transform.right * 10.0f;
        }

        private void InitDispenseCooldown()
        {
            dispenseCooldown = Random.Range(initMinDispenseInterval, maxDispenseInterval);
        }

        private void ResetDispenseCooldown()
        {
            dispenseCooldown = Random.Range(minDispenseInterval, maxDispenseInterval);
        }
    }
}