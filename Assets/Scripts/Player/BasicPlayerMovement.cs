using Unity.Mathematics.Geometry;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
    public class BasicPlayerMovement : MonoBehaviour
    {
        [SerializeField] private string groundTag = "Ground";
        
        private Rigidbody2D _rb;
        private AudioSource _audioSource;
        private float _xInput;
        [SerializeField] private float speed = 5;

        private bool _performJump;
        private bool _isGrounded;
        [SerializeField] private float jumpForce = 5;
        [SerializeField] private AudioClip jumpSound;
        
        [SerializeField] private int maxJumps = 2;
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
            
            jumpsRemaining = maxJumps;
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
                    jumpsRemaining--;
                    _audioSource.PlayOneShot(jumpSound);
                }
                else if (jumpsRemaining == 1)
                {
                    _performJump = true;
                    jumpsRemaining--;
                    _audioSource.PlayOneShot(jumpSound);
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
                        jumpsRemaining = maxJumps;
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
    }
}