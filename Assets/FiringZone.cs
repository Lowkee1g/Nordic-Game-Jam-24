using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringZone : MonoBehaviour
{
    private GameObject playerObject;
    private laser laserScript;
    private GameObject playerSpriteObject;
    private SpriteRenderer playerSpriteRenderer;

    public Sprite canShootSprite; // Assign the can shoot sprite in the Unity Editor
    public Sprite cannotShootSprite; // Assign the cannot shoot sprite in the Unity Editor


    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        laserScript = playerObject.GetComponent<laser>();
        playerSpriteObject = GameObject.FindGameObjectWithTag("PlayerSprite");
        playerSpriteRenderer = playerSpriteObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlayerSprite(canShootSprite);
            EnableLaserScript();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlayerSprite(cannotShootSprite);
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
                laserScript.canShoot = false;
                
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
                laserScript.canShoot = true;
            }
        }
    }

        private void SetPlayerSprite(Sprite newSprite)
    {
        if (playerSpriteRenderer != null)
        {
            playerSpriteRenderer.sprite = newSprite;
        }
    }
}