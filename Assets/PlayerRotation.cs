using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Get the mouse position
        Vector2 mousePosition = GetWorldPositionAtDepth(Input.mousePosition, 0f);

        // Calculate rotation towards mouse position
        Vector2 direction = mousePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the object towards the mouse position
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private Vector3 GetWorldPositionAtDepth(Vector3 screenPosition, float depth)
    {
        Camera mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, depth));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
