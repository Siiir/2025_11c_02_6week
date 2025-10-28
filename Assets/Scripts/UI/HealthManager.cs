using UnityEngine;
using damage; // your Hurtable namespace

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Hurtable hurtable;      // assign in Inspector
    [SerializeField] private RectTransform fillPanel; // the inner fill bar

    private float maxWidth;

    private void Start()
    {
        if (hurtable == null || fillPanel == null)
        {
            Debug.LogError("Missing references on HealthBar!");
            return;
        }

        // Remember the bar's original width (full health)
        maxWidth = fillPanel.sizeDelta.x;
        UpdateBar();
    }

    private void Update()
    {
        if (hurtable == null) return;
        UpdateBar();
    }

    private void UpdateBar()
    {
        float healthPercent = (float)hurtable.Health / hurtable.FullHealth;
        Vector2 size = fillPanel.sizeDelta;
        size.x = maxWidth * healthPercent;
        fillPanel.sizeDelta = size;
    }
}