using UnityEngine;

public class Flag : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Raise = Animator.StringToHash("Raise");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetTrigger(Raise);
        }
    }
}