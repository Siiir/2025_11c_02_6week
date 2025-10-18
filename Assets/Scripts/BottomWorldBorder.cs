using UnityEngine;

public class BottomWorldBorder : MonoBehaviour
{
    public static float Y { get; private set; } = float.NaN;

    private void Awake()
    {
        Y = transform.position.y;
    }
}