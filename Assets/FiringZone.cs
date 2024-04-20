using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringZone : MonoBehaviour
{
    private GameObject playerObject;
    private laser laserScript;


    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        laserScript = playerObject.GetComponent<laser>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnableLaserScript();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             DisableLaserScript();
        }
    }

    private void DisableLaserScript()
    {
        // Check if playerObject is not null before attempting to access its components
        if (playerObject != null)
        {
            // Get the "laser" script attached to the player object and disable it
            if (laserScript != null)
            {
                laserScript.enabled = false;
            }
        }
    }

    private void EnableLaserScript()
    {
        // Check if playerObject is not null before attempting to access its components
        if (playerObject != null)
        {
            // Get the "laser" script attached to the player object and enable it
            if (laserScript != null)
            {
                laserScript.enabled = true;
            }
        }
    }
}