using UnityEngine;

namespace death_effects
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] private float lifeTimeLeft = 3f;

        void Start()
        {
            Destroy(gameObject, lifeTimeLeft);
        }
    }
}