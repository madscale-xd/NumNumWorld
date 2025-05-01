using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Flyer object
    public float smoothSpeed = 0.125f;
    public Vector3 offset;    // Distance from camera to player

    void LateUpdate()
    {
        // We only follow in the X axis, not the Y
        float desiredX = target.position.x + offset.x;
        float desiredY = transform.position.y;  // Don't change Y position
        float desiredZ = transform.position.z;  // Keep camera's Z

        Vector3 desiredPosition = new Vector3(desiredX, desiredY, desiredZ);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}