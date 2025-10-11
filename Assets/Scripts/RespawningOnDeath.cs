using UnityEngine;

public class RespawningOnDeath : Mortal
{
    // Fields
    [SerializeField] private Transform respawnTransform;
    // Methods
    public override void Die()
    {
        this.gameObject.transform.position = this.respawnTransform.position;
    }
}