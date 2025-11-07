using System;
using UnityEngine;

namespace damage.hurting
{
    public class Hurts : Hurtable
    {
        // Version of the protected field EXPOSED for assignment in the inspector
        [SerializeField] private HitPoints hitPoints;

        protected override void Awake()
        {
            base.Awake();
            HitPoints = hitPoints ?? GetComponentInParent<HitPoints>();
            if (HitPoints == null)
            {
                throw new Exception(
                    "Target HitPoints component not found in the parent hierarchy, nor assigned in the inspector.");
            }
        }
    }
}