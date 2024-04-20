using UnityEngine;

public class laser : MonoBehaviour
{
    // create a game object called bullet
    public GameObject bullet;

    private GameObject bulletClone = null;


    // Update is called once per frame
    void Update()
    {
        // throw the bullet in the direction of the laser using the bullet's rigidbody 2d   
        if (Input.GetMouseButtonDown(0) && bulletClone == null)
        {
            bulletClone = Instantiate(bullet, transform.position + transform.up, transform.rotation);
            bulletClone.GetComponent<Rigidbody2D>().velocity = transform.up * 5;
        }


        
    }

}
