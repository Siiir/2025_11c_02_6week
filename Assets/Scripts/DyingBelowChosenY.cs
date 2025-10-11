using UnityEngine;

[RequireComponent(typeof(Mortal))]
public class DyingBelowChosenY : MonoBehaviour
{
    
    // Fields
    
    private const float DieWhenBelow = 0;
    
    private Mortal _mortal;

    // Methods
    
    private void Awake()
    {
        this._mortal = this.gameObject.GetComponent<Mortal>();
    }

    private void Update()
    {
        if (this.gameObject.transform.position.y < DieWhenBelow)
        {
            this._mortal.Die();
        }
    }
}