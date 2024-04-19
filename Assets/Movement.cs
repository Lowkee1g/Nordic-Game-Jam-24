using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Rigidbody _rb;
    Vector2 _movement;  

     // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
     _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));   
    }


    private void FixedUpdate()
    {
        moveCharacter(_movement);
    }
    void moveCharacter(Vector2 direction)
    {
        _rb.AddForce(direction * speed);
    }
}
