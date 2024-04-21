using UnityEngine;

public class laser : MonoBehaviour
{
    // create a game object called bullet
    public GameObject bullet;

    private GameObject bulletClone = null;

    public GameObject tracerBullet;

    private GameObject tracerBulletClone = null;

    public bool canShoot = false;


    // Update is called once per frame
    void Update()
    {
        // throw the bullet in the direction of the laser using the bullet's rigidbody 2d   
        if (Input.GetKeyUp(KeyCode.Space) && canShoot == true && bulletClone == null && tracerBulletClone == null){
            bulletClone = Instantiate(bullet, transform.position + GetDirection(), transform.rotation);
            bulletClone.GetComponent<Rigidbody2D>().velocity = GetDirection() * 5;
        }

        //shoots tracer bullet when t is pressed
        if (Input.GetKeyUp(KeyCode.T) && tracerBulletClone == null)
        {
            tracerBulletClone = Instantiate(tracerBullet, transform.position + GetDirection(), transform.rotation);
            tracerBulletClone.GetComponent<Rigidbody2D>().velocity = GetDirection() * 5;
        }   


  
    }

    // get a normalized vector from the laser to the mouse position using getWorldPositionAtDepth vector3
    private Vector3 GetDirection()
    {
        Vector3 mousePosition = GetWorldPositionAtDepth(Input.mousePosition, 0f);
        return (mousePosition - transform.position).normalized;
    }

    

    private Vector3 GetWorldPositionAtDepth(Vector3 screenPosition, float depth)
    {
        Camera mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, depth));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
