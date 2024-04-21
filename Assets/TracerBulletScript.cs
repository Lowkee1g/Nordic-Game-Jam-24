using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerBulletScript : MonoBehaviour
{
    public CircleCollider2D circleCollider;

    private void Start() {
        //findes the collider that is not a trigger and sets it to the circleCollider
        foreach (CircleCollider2D collider in GetComponents<CircleCollider2D>()) {
            if (collider.sharedMaterial != null && collider.sharedMaterial.name == "Bounce") {
                circleCollider = collider;
                
                break;
            }
        }

      

        
    }


     // destroy the bullet clone when it collides with objects with out the tag "Spejl"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Glass")
        {
            circleCollider.isTrigger = true;
            circleCollider.enabled = false;
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy") {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Glass") {
            circleCollider.isTrigger = false;
            circleCollider.enabled = true;
        }
        
    }

   

}
