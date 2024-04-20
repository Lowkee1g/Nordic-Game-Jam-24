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
        foreach (Transform waypoint in path)
        {
            Debug.Log(waypoint.name);
        }
        // Reverse the path for the children objects of rute
        //path.Reverse();

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
                // Move towards the next waypoint
                while (transform.position != waypoint.transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, Time.deltaTime * speed);
                    yield return null;

                }

                yield return new WaitForSeconds(waypoint.localEulerAngles.x);

                
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
            path.Reverse();
        }

      
        // Path completed
        Debug.Log("Path completed!");
    }
    

    private List<Transform> FindPath()
    {
        // Find the path from start to finish
        //List<GameObject> path = new List<GameObject>();

        List<Transform> path = new List<Transform>(rute.transform.GetComponentsInChildren<Transform>());
        path.RemoveAt(0);

        return path;
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

