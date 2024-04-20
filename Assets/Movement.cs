using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 100f;
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
        rb.velocity =  movement * speed * Time.fixedDeltaTime;
        //rotation
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        rb.rotation = angle;       
    }

}
