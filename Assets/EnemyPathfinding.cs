using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    public GameObject rute;
    public float speed = 5f;
    private Transform currentWaypoint;

    private List<Transform> path;

    private void Start()
    {
        // Find the path from start to finish
        path = FindPath();

        // Reverse the path for the children objects of rute
        //path.Reverse();

        // Move along the path
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach (Transform waypoint in path)
        {
            currentWaypoint = waypoint;
            // Move towards the next waypoint
            while (transform.position != waypoint.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, Time.deltaTime * speed);
                yield return null;
            }
        }

        // Path completed
        Debug.Log("Path completed!");
    }

    private List<Transform> FindPath()
    {
        // Find the path from start to finish
        //List<GameObject> path = new List<GameObject>();

        return new List<Transform>(rute.transform.GetComponentsInChildren<Transform>());
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag != "point" || other.transform != currentWaypoint)
        {
            return;
        }
        this.transform.position = other.transform.position;
    }
}

