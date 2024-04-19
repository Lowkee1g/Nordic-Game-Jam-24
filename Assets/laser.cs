using UnityEngine;

public class laser : MonoBehaviour
{
    public int maxReflectionCount = 5;
    public float maxRayDistance = 100f;
    public LayerMask layerMask; // Layer mask to selectively ignore colliders

    private void Update()
    {
        DrawReflectedRay(transform.position, transform.up, maxReflectionCount);
    }

    private void DrawReflectedRay(Vector2 position, Vector2 direction, int reflectionsRemaining)
    {
        if (reflectionsRemaining == 0)
            return;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, maxRayDistance, layerMask);

        // Draw the ray for debugging purposes
        Debug.DrawRay(position, direction * maxRayDistance, Color.red);

        if (hit.collider != null)
        {
            // Draw the incoming ray
            Debug.DrawLine(position, hit.point, Color.red);

            // Reflect the ray off the surface
            direction = Vector2.Reflect(direction, hit.normal);
            position = hit.point;

            // Draw the reflected ray from the hit point in the direction of reflection
            Debug.DrawRay(position, direction * maxRayDistance, Color.green);

            // Recursive call to draw the next ray
            DrawReflectedRay(position, direction, reflectionsRemaining - 1);
        }
    }
}
