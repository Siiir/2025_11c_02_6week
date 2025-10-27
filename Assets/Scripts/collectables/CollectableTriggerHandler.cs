using UnityEngine;

public class CollectableTriggerHandler : MonoBehaviour
{
    private Collectable _collectable;

    private Animator _animator;
    private static readonly int Collected = Animator.StringToHash("Collected");
    private bool _isCollected;

    private void Awake()
    {
        _collectable = GetComponent<Collectable>();
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_isCollected)
            return;

        if (other.CompareTag("Player"))
        {
            _isCollected = true;
            _collectable.Collect(other.gameObject);
            _animator.SetTrigger(Collected);
            Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}