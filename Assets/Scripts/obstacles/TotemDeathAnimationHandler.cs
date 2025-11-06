using UnityEngine;
using death_processors;
using death_effects;

[RequireComponent(typeof(Terminable))]
public class TotemDeathAnimationHandler : MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash("Die");
    [SerializeField] private Animator animator;
    [SerializeField] private float timeBeforeDestruction = 0.5f;
    [SerializeField] private GameObject totemShattered;

    private Terminable _terminable;
    private bool _hasCollided;
    
    private void Awake()
    {
        _terminable = GetComponent<Terminable>();
    }

    public void PlayDeathAnimation()
    {
        if (animator)
        {
            animator.SetTrigger(Die);
        }

        if (!_hasCollided)
        {
            Instantiate(totemShattered, transform.position, transform.rotation);
            _hasCollided = true;
        }

        Invoke(nameof(CallTerminate), timeBeforeDestruction);
    }

    private void CallTerminate()
    {
        _terminable.Terminate();
    }
}