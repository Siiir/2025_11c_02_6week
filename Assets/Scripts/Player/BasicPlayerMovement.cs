using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicPlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _xInput;
    [SerializeField] private float speed = 5;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _xInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_xInput * speed, _rb.linearVelocity.y);
    }
}