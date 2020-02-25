using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_2D : MonoBehaviour {

    private Rigidbody2D rigidBody2D;
    
    [SerializeField] private float max_speed;
    [SerializeField] private float jump_Force;
    //[SerializeField] private float movement_scalar;

    // Start is called before the first frame update
    void Start() {
        rigidBody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    // FixedUpdate is called at a fixed time interval
    void FixedUpdate() {
        float x_movement = Input.GetAxis("Horizontal");

        float move_by = x_movement * max_speed;

        rigidBody2D.velocity = new Vector2(move_by, rigidBody2D.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space)) {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jump_Force);
        }

        //if (rigidBody2D.velocity.magnitude < max_speed) {
        //    Vector2 movement = new Vector2(x_movement, 0);
        //    rigidBody2D.AddForce(movement_scalar * movement);
        //}
    }
}
