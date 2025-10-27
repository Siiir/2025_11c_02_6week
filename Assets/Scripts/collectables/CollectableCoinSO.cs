using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectableCoinSO", menuName = "Scriptable Objects/CollectableCoinSO")]
public class CollectableCoinSO : CollectableSOBase
{
    
    [Header("Stats")] public int value = 1;
    [Header("Content")] public AudioClip audioClip;
    
    public static event Action<int> OnCoinCollected;
    
    public override void Collect(GameObject collector)
    {
        collector.GetComponent<AudioSource>().PlayOneShot(audioClip);
        OnCoinCollected?.Invoke(value);
    }
}
