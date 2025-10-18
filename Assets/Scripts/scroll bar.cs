using UnityEngine;

public class ScrollLockYElastic : MonoBehaviour
{
    [Header("Clamp Y Range")]
    public float maxY = 1250f;    // Top boundary
    public float minY = -1700f;   // Bottom boundary

    [Header("Elastic Settings")]
    public float bounceSpeed = 10f;   // How fast it returns
    public float elasticity = 0.3f;   // Smoothness of bounce (0.1 = soft, 1 = snappy)

    private RectTransform rectTransform;
    private Vector3 targetPos;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Set the initial starting position
        Vector3 startPos = rectTransform.anchoredPosition;
        startPos.y = minY;
        rectTransform.anchoredPosition = startPos;
    }

    void LateUpdate()
    {
        Vector3 pos = rectTransform.anchoredPosition;

        // Allow normal movement within bounds
        if (pos.y <= maxY && pos.y >= minY)
        {
            targetPos = pos;
        }
        else
        {
            // Clamp if it exceeds range
            float clampedY = Mathf.Clamp(pos.y, minY, maxY);
            targetPos = new Vector3(pos.x, clampedY, pos.z);
        }

        // Smooth elastic return
        rectTransform.anchoredPosition = Vector3.Lerp(
            rectTransform.anchoredPosition,
            targetPos,
            Time.deltaTime * bounceSpeed
        );
    }
}

//this is a demo project for unity
//this is a demo project for unity