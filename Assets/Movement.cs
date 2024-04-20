using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Rigidbody2D rb;
    Vector2 movement;
     // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }    // Update is called once per frame

    void Update()
    {
        // Input
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    private void FixedUpdate()

        // Phyisics
    {   
        moveCharacter();
    }

    
    void moveCharacter(){
        if (movement.magnitude != 0){
        rb.velocity =  movement * speed * Time.fixedDeltaTime;
        // float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else{
            rb.velocity = Vector2.zero;
        }
        
    }

}
