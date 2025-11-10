using death_processors;
using UnityEngine;

[RequireComponent(typeof(Terminable))]
public class TotemDeathAnimationHandler : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestruction = 0.1f;
    [SerializeField] private GameObject totemShattered;

    private Terminable _terminable;
    private bool _hasCollided;

    private void Awake()
    {
        _terminable = GetComponent<Terminable>();
    }

    public void PlayDeathAnimation()
    {
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