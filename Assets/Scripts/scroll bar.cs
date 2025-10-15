using UnityEngine;

public class ScrollLockYElastic : MonoBehaviour
{
    [Header("Clamp Y Range")]
    public float maxY = 1250f;    // Top boundary
    public float minY = -1600f;   // Bottom boundary

    [Header("Elastic Settings")]
    public float bounceSpeed = 10f;  // How fast it returns to limits
    public float elasticity = 0.3f;  // How soft the bounce is (0.1 = very soft, 1 = hard snap)

    private RectTransform rectTransform;
    private Vector3 targetPos;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        Vector3 pos = rectTransform.anchoredPosition;

        // If within limits, let it move freely
        if (pos.y <= maxY && pos.y >= minY)
        {
            targetPos = pos;
        }
        else
        {
            // If out of range, set bounce-back target
            float clampedY = Mathf.Clamp(pos.y, minY, maxY);
            targetPos = new Vector3(pos.x, clampedY, pos.z);
        }

        // Smoothly move toward target (bounce effect)
        rectTransform.anchoredPosition = Vector3.Lerp(
            rectTransform.anchoredPosition,
            targetPos,
            Time.deltaTime * bounceSpeed
        );
    }
}
