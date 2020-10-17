using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 10f;
    public Vector3 offset;
    public float maxX = 1000.0f;
    public float minX = -1000.0f;
    public float maxY = 500.0f;
    public float minY = -500.0f;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        if(desiredPosition.x > maxX) { desiredPosition.x = maxX; }
        if(desiredPosition.x < minX) { desiredPosition.x = minX; }
        if(desiredPosition.y > maxY) { desiredPosition.y = maxY; }
        if(desiredPosition.y < minY) { desiredPosition.y = minY; }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
