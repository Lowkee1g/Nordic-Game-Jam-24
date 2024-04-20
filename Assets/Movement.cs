using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private AudioSource sound;

    private SpritePlacer spritePlacer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        spritePlacer = GameObject.Find("Camera").GetComponent<SpritePlacer>();
        

    }

    // Update is called once per frame
    void Update()
    {
        // Input
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKeyUp(KeyCode.Z))
        {
            CastRayToMouse();
        }
    }

    private void FixedUpdate()
    {
        // Physics
        moveCharacter();
    }

    void moveCharacter()
    {
        // Play sound if moving, stop if not
        if (sound.isPlaying == false && movement.magnitude != 0)
        {
            sound.Play();
        }
        else if (movement.magnitude == 0)
        {
            sound.Stop();
        }

        rb.velocity = movement * speed * Time.fixedDeltaTime;
    }

   private void CastRayToMouse()
    {
         
        // Get the mouse position in world space
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        // Calculate the direction from the player to the mouse position
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;
        
        // Define the maximum range of the ray
        float maxRayDistance = 1000f; // Set this to whatever you need

        // Cast the ray from the player's position towards the direction of the mouse
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right, direction, maxRayDistance);

        
        // Check if something was hit
        if (hit.collider != null && hit.collider.gameObject.tag == "Spejl")
        {
            Debug.Log("Spejl hit: " + hit.collider.gameObject.name);
            Destroy(hit.collider.gameObject);
            spritePlacer.currentSpejles--;
        }
    }
}