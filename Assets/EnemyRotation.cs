using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    public Rigidbody2D rb2d; // Assign this in the inspector or find it during Start
    public float rotationSpeed = 5f; // Adjust this to make rotation smoother
    
    void Start()
    {
        if (rb2d == null) // Find Rigidbody2D if not assigned
        {
            rb2d = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        RotateTowardsMovementDirection();
    }

    void RotateTowardsMovementDirection()
    {
        if (rb2d.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
