using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    private Transform target; // The target the camera should follow
    public Transform camTarget; // The target the camera should follow
    public float smoothSpeed = 0.125f; // Adjust this to change how quickly the camera follows
    public Vector3 offset; // The offset from the target
    public Vector3 BulletOffset; // The offset from the target

    // lock the camera to the player at the start
    void Start()
    {
        target = camTarget;
    }


    void LateUpdate()
    {
        // if there is a bullet in the scene, set the target to the bullet
        if (GameObject.Find("Bullet(Clone)") != null)
        {
            target = GameObject.Find("Bullet(Clone)").transform;
            Vector3 desiredPosition = target.position + BulletOffset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * 4 * Time.deltaTime);
            transform.position = smoothedPosition;
        } else {
            // if there is no bullet in the scene, set the target to the player
            target = camTarget;
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }

        // Optionally, you can make the camera always look at the target:
        //transform.LookAt(target);
    }
}
