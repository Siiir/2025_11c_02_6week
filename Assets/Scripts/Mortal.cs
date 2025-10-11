using UnityEngine;

public class Mortal : MonoBehaviour
{
    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
