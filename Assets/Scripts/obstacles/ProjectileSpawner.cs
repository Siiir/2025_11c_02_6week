using UnityEngine;

namespace obstacles
{
    [RequireComponent(typeof(AudioSource))]
    public class ProjectileSpawner : MonoBehaviour
    {
        // References
        [SerializeField] private GameObject projectilePrefab;

        [SerializeField] private AudioClip shotSound;

        // Constants
        [SerializeField] private bool disableRendering = true;
        [SerializeField] private float minDispenseInterval = 2.0f;
        [SerializeField] private float maxDispenseInterval = 2.4f;

        // Variables
        [Tooltip("Initial dispense cooldown in seconds. NaN to randomize on start.")] [SerializeField]
        private float dispenseCooldown = float.NaN;

        // Components
        private AudioSource _audioSource;

        private void Awake()
        {
            if (float.IsNaN(dispenseCooldown))
            {
                ResetDispenseCooldown();
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
                DispenseProjectile();
                ResetDispenseCooldown();
            }
        }

        private void DispenseProjectile()
        {
            _audioSource.PlayOneShot(shotSound);
            var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            // Apply initial force/velocity
            var rb = projectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = (Vector2)transform.right * 10.0f;
        }

        private void ResetDispenseCooldown()
        {
            dispenseCooldown = Random.Range(minDispenseInterval, maxDispenseInterval);
        }
    }
}