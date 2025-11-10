using damage;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthBar : MonoBehaviour
{
    [FormerlySerializedAs("hp")] [SerializeField]
    private HitPoints heroHp;

    [SerializeField] private RectTransform fillPanel;

    private float maxWidth;

    private void Start()
    {
        if (heroHp == null || fillPanel == null)
        {
            Debug.LogError("Missing references on HealthBar!");
            return;
        }

        maxWidth = fillPanel.sizeDelta.x;
        UpdateBar();
    }

    private void Update()
    {
        if (heroHp == null) return;
        UpdateBar();
    }

    private void UpdateBar()
    {
        float healthPercent = (float)heroHp.Health / heroHp.FullHealth;
        Vector2 size = fillPanel.sizeDelta;
        size.x = maxWidth * healthPercent;
        fillPanel.sizeDelta = size;
    }
}