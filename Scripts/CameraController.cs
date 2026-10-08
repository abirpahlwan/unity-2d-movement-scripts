using UnityEngine;

/// <summary>
/// CameraController provides smooth 2D camera tracking for the player character with optional boundary clamping.
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Target Tracking")]
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("Camera Bounding Box (Optional)")]
    [SerializeField] private bool enableBounds = false;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    private Vector3 currentVelocity = Vector3.zero;

    private void Start()
    {
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) target = playerObj.transform;
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;

        if (enableBounds)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}