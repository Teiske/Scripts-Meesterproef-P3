using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_2D : MonoBehaviour {

    private Rigidbody2D rigidBody2D;
    
    [SerializeField] private float max_speed;
    [SerializeField] private float jump_Force;
    [SerializeField] private float fall_multiplier;
    [SerializeField] private float low_jump_multiplier;
    //[SerializeField] private float movement_scalar;

    [SerializeField] private bool is_grounded = false;
    [SerializeField] private Transform is_ground_checker;
    [SerializeField] private float check_ground_radius;
    [SerializeField] private LayerMask ground_layer;

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

        if (Input.GetKey(KeyCode.Space) && is_grounded) {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jump_Force);
        }

        Collider2D collider = Physics2D.OverlapCircle(is_ground_checker.position, check_ground_radius, ground_layer);

        if (collider != null) {
            is_grounded = true;
        }
        else {
            is_grounded = false;
        }

        if (rigidBody2D.velocity.y < 0) {
            rigidBody2D.velocity += Vector2.up * Physics2D.gravity * (fall_multiplier - 1) * Time.deltaTime;
        }
        else if (rigidBody2D.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rigidBody2D.velocity += Vector2.up * Physics2D.gravity * (low_jump_multiplier - 1) * Time.deltaTime;
        }
        //if (rigidBody2D.velocity.magnitude < max_speed) {
        //    Vector2 movement = new Vector2(x_movement, 0);
        //    rigidBody2D.AddForce(movement_scalar * movement);
        //}
    }
}
