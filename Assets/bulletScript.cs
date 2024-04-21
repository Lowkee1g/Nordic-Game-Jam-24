using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // get the GameManagers script
    public gamemanager gm;

    // Start is called before the first frame update
    void Start()
    {
        // get the GameManagers script
        gm = GameObject.Find("GameManager").GetComponent<gamemanager>();
    }

     // destroy the bullet clone when it collides with objects with out the tag "Spejl"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Glass")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy") {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Goon")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            gm.KillPlayer("You shot the wrong person!");
        }

    }

}
