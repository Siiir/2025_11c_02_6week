using UnityEngine;

public class CollectableTriggerHandler : MonoBehaviour
{
    private Collectable _collectable;

    private void Awake()
    {
        _collectable = GetComponent<Collectable>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _collectable.Collect(other.gameObject);
            Destroy(gameObject);
        }
    }
}