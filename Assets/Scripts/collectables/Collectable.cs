using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(CollectableTriggerHandler))]
public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableSOBase collectableSo;

    public void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void Collect(GameObject collector)
    {
        collectableSo.Collect(collector);
    }
}