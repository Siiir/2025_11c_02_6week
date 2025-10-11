using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicPlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _xInput;
    [SerializeField] private float speed = 5;

    private bool _performJump;
    private bool _isGrounded;
    [SerializeField] private float jumpForce = 5;

    private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _xInput = Input.GetAxis("Horizontal");

        if (_isGrounded)
        {
            _coyoteTimeCounter = _coyoteTime;
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
        _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isGrounded = false;
    }
}