using damage;
using death_effects.interfaces;
using death_processors;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D), typeof(AudioSource), typeof(AgonyfulMortal))]
    public class BasicPlayerMovement : MonoBehaviour, IDamagableComponent, IPostDeath
    {
        [SerializeField] private string groundTag = "Ground";

        private Rigidbody2D _rb;
        private AudioSource _audioSource;
        private float _xInput;
        [SerializeField] private float speed = 5;

        private bool _performJump;
        private bool _isGrounded;
        [SerializeField] private float jumpForce = 5;
        [SerializeField] private AudioClip jumpSound1;
        [SerializeField] private AudioClip jumpSound2;
        [SerializeField] private AudioClip jumpSound3;

        [FormerlySerializedAs("maxJumps")] [SerializeField] private int doubleJumps = 2;
        [SerializeField] private int jumpsRemaining;

        [SerializeField] private float coyoteTime = 0.4f;
        private float _coyoteTimeCounter;

        [SerializeField] private float surfaceNormal = 0.5f;

        public bool FacingRight { get; private set; } = true;
        private SpriteRenderer _spriteRenderer;

        private Animator _animator;
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int XInputAbs = Animator.StringToHash("XInputAbs");

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            jumpsRemaining = doubleJumps;
        }

        private void Update()
        {
            _xInput = Input.GetAxisRaw("Horizontal");

            // Update Facing direction
            if (_xInput > 0) FacingRight = true;
            else if (_xInput < 0) FacingRight = false;
            if (_spriteRenderer != null)
                _spriteRenderer.flipX = !FacingRight;

            if (_isGrounded) 
            {
                _coyoteTimeCounter = coyoteTime;
                if (_rb.linearVelocity.y <= 0)
                {
                    jumpsRemaining = maxJumps;
                }
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (_isGrounded || _coyoteTimeCounter > 0f)
                {
                    _performJump = true;
                }
                else if (jumpsRemaining > 0)
                {
                    _performJump = true;
                    jumpsRemaining--;
                }

                if (_performJump)
                {
                    var clip = GetWeightedJumpSound();
                    if (clip != null)
                        _audioSource.PlayOneShot(clip,0.5f);
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                _coyoteTimeCounter = 0;
            }

            if (_animator != null)
            {
                SetAnimationState();
            }
        }

        private void SetAnimationState()
        {
            const float threshold = 0.1f;

            if (_rb.linearVelocity.y > threshold)
            {
                _animator.SetBool(IsJumping, true);
                _animator.SetBool(IsFalling, false);
                return;
            }

            if (_rb.linearVelocity.y < -threshold)
            {
                _animator.SetBool(IsJumping, false);
                _animator.SetBool(IsFalling, true);
                return;
            }

            _animator.SetBool(IsJumping, false);
            _animator.SetBool(IsFalling, false);
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = new Vector2(_xInput * speed, _rb.linearVelocity.y);

            if (_performJump)
            {
                _performJump = false;
                _isGrounded = false;

                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
            }

            if (_animator != null)
            {
                _animator.SetFloat(XInputAbs, Mathf.Abs(_xInput));
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(groundTag))
            {
                foreach (var contact in collision.contacts)
                {
                    if (contact.normal.y > surfaceNormal)
                    {
                        _isGrounded = true;
                        jumpsRemaining = doubleJumps;
                        return;
                    }
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(groundTag))
            {
                foreach (var contact in collision.contacts)
                {
                    if (contact.normal.y > surfaceNormal)
                    {
                        _isGrounded = true;
                        return;
                    }
                }

                _isGrounded = false;
            }
        }


        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(groundTag))
            {
                _isGrounded = false;
            }
        }

        public void DoPostDeath()
        {
            // Player should not be able to control character after death
            //
            // This is a bad fix because class does not adhere to SRP principle.
            // https://en.wikipedia.org/wiki/SOLID
            // However, I will not fix the class. It might be fine anyway.
            this.enabled = false;
        }

        public void RestoreDamage()
        {
            this.enabled = true;
        }
        
        private AudioClip GetWeightedJumpSound()
        {
            float weight1 = 0.5f;
            float weight2 = 0.495f;
            // last ones weight is 1-(w1+w2)
            
            float rand = Random.value;
            if (rand < weight1)
                return jumpSound1;
            else if (rand < weight1 + weight2)
                return jumpSound2;
            else
                return jumpSound3;
        }

    }
    
    
    
}