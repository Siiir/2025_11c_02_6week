using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("Open");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetTrigger(Open);
        }
    }
}
