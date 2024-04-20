using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerBulletScript : MonoBehaviour
{
     // destroy the bullet clone when it collides with objects with out the tag "Spejl"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
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

}
