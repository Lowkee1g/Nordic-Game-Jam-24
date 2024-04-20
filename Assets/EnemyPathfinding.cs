using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    public GameObject route;
    public float speed = 5f;
    public float rotationSpeed = 200f; // Speed at which the enemy rotates towards the next waypoint

    private Transform currentWaypoint;
    private Transform circleTransform; // Transform of the circle object
    private List<Transform> path;


    private void Start()
    {
        // Find the Circle child object
        circleTransform = transform.Find("Circle");
        if (circleTransform == null)
        {
            Debug.LogError("Circle child object not found!");
            return;
        }

        // Find the path from start to finish
        path = FindPath();
    
        // Move along the path

       
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        while (true)
        {
            foreach (Transform waypoint in path)
            {
                currentWaypoint = waypoint;
                while (transform.position != waypoint.position)
                {
                    // Determine the direction to the next waypoint
                    Vector3 direction = (waypoint.position - transform.position).normalized;

                    // Rotate only the Circle child object
                    if (direction != Vector3.zero)
                    {
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward); // Adjusted for sprite forward
                        circleTransform.rotation = Quaternion.RotateTowards(circleTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    }

                    transform.position = Vector3.MoveTowards(transform.position, waypoint.position, Time.deltaTime * speed);
                    yield return null;

                }
            }

            path.Reverse();

            foreach (Transform waypoint in path)
            {
                currentWaypoint = waypoint;
                // Move towards the next waypoint
                while (transform.position != waypoint.transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, Time.deltaTime * speed);
                    yield return null;
                }

                if(path.IndexOf(waypoint) != path.Count - 1 && path.IndexOf(waypoint) != 0)
                {
                    yield return new WaitForSeconds(waypoint.localEulerAngles.x);
                }
                
            }

            // Reverse the path
            path.Reverse();
        }

      
        // Path completed
    }

    private List<Transform> FindPath()
    {
        List<Transform> path = new List<Transform>(route.GetComponentsInChildren<Transform>());
        path.RemoveAt(0); // Remove the route itself, leaving only its waypoints

        return path;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "point" || other.transform != currentWaypoint)
        {
            return;
        }
        transform.position = other.transform.position;
    }
}
