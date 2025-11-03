using UnityEngine;
using damage;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Hurtable hurtable;
    [SerializeField] private RectTransform fillPanel;

    private float maxWidth;

    private void Start()
    {
        if (hurtable == null || fillPanel == null)
        {
            Debug.LogError("Missing references on HealthBar!");
            return;
        }

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