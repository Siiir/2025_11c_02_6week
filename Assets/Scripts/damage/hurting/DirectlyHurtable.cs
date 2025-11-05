using UnityEngine;

namespace damage.hurting
{
    [RequireComponent(typeof(HitPoints))]
    public class DirectlyHurtable : Hurtable
    {
        protected override void Awake()
        {
            base.Awake();
            HitPoints = GetComponent<HitPoints>();
            Debug.Assert(HitPoints != null);
        }
    }
}