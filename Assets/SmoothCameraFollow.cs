using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // The target the camera should follow
    public float smoothSpeed = 0.125f; // Adjust this to change how quickly the camera follows
    public Vector3 offset; // The offset from the target

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Optionally, you can make the camera always look at the target:
        //transform.LookAt(target);
    }
}
