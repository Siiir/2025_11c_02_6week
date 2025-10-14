using UnityEngine;

[CreateAssetMenu(fileName = "CollectableSOBase", menuName = "Scriptable Objects/CollectableSOBase")]
public abstract class CollectableSOBase : ScriptableObject
{
    public abstract void Collect(GameObject collector);
}
