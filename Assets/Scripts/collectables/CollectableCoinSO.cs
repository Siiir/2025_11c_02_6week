using UnityEngine;

[CreateAssetMenu(fileName = "CollectableCoinSO", menuName = "Scriptable Objects/CollectableCoinSO")]
public class CollectableCoinSO : CollectableSOBase
{
    [Header("Stats")] public int value = 1;
    public override void Collect(GameObject collector)
    {
        
    }
}
