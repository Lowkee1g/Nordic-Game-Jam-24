using UnityEngine;

public class laser : MonoBehaviour
{
    // create a game object called bullet
    public GameObject bullet;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        // throw the bullet in the direction of the laser using the bullet's rigidbody 2d   
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletClone = Instantiate(bullet, transform.position, transform.rotation);
            bulletClone.GetComponent<Rigidbody2D>().velocity = transform.up * 5;
        }
        
    }
}
