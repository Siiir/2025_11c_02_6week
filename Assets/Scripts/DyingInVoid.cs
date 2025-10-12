using System;
using UnityEngine;

[RequireComponent(typeof(Mortal))]
public class DyingInVoid : MonoBehaviour
{
    
    // Fields
    
    private const String BottomDeathLineName = "BottomDeathLine";
    
    private static readonly Lazy<float> DieWhenBelow = new Lazy<float>(() =>
    {
        GameObject bottomDeathLine = GameObject.Find(BottomDeathLineName);
        if (bottomDeathLine == null)
        {
            throw new Exception($"{BottomDeathLineName} not found in the simulation!");
        }
        return bottomDeathLine.transform.position.y;
    });
    
    private Mortal _mortal;

    // Methods
    
    private void Awake()
    {
        this._mortal = this.GetComponent<Mortal>();
    }

    private void Update()
    {
        if (this.transform.position.y < DieWhenBelow.Value)
        {
            this._mortal.Die();
        }
    }
}