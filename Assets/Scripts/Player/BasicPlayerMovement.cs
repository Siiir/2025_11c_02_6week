using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BasicPlayerMovement : MonoBehaviour
    {
        [SerializeField] private string groundTag = "Ground";
        
        private Rigidbody2D _rb;
        private float _xInput;
        [SerializeField] private float speed = 5;

        private bool _performJump;
        private bool _isGrounded;
        [SerializeField] private float jumpForce = 5;

        [SerializeField] private float coyoteTime = 0.4f;
        private float _coyoteTimeCounter;
        
        [SerializeField] private float surfaceNormal = 0.5f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _xInput = Input.GetAxis("Horizontal");

            if (_isGrounded)
            {
                _coyoteTimeCounter = coyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }

            if (Input.GetButtonDown("Jump") && _coyoteTimeCounter > 0)
            {
                _performJump = true;
            }

            if (Input.GetButtonUp("Jump"))
            {
                _coyoteTimeCounter = 0;
            }
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