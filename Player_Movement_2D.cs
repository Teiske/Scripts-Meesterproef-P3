using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player_Movement_2D : MonoBehaviour {

    private Rigidbody2D rigidBody2D;
    
    [SerializeField] private float max_speed;
    [SerializeField] private float jump_Force;
    [SerializeField] private float fall_multiplier;
    [SerializeField] private float low_jump_multiplier;

    [SerializeField] private bool is_grounded = false;
    [SerializeField] private Transform is_ground_checker;
    [SerializeField] private float check_ground_radius;
    [SerializeField] private LayerMask ground_layer;

    [SerializeField] private float remember_grounded_for;
    [SerializeField] private float last_time_grounded;

    // Start is called before the first frame update
    void Start() {
        rigidBody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    // FixedUpdate is called at a fixed time interval
    void FixedUpdate() {
        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
    }

    private void Move() {
        float x_movement = Input.GetAxis("Horizontal");
        float move_by = x_movement * max_speed;
        rigidBody2D.velocity = new Vector2(move_by, rigidBody2D.velocity.y);
    }

    private void Jump() {
        if (Input.GetKey(KeyCode.Space) && (is_grounded || Time.time - last_time_grounded <= remember_grounded_for)) {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jump_Force);
        }
    }

    private void BetterJump() {
        if (rigidBody2D.velocity.y < 0) {
            rigidBody2D.velocity += Vector2.up * Physics2D.gravity * (fall_multiplier - 1) * Time.deltaTime;
        }
        else if (rigidBody2D.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rigidBody2D.velocity += Vector2.up * Physics2D.gravity * (low_jump_multiplier - 1) * Time.deltaTime;
        }
    }

    private void CheckIfGrounded() {
        Collider2D collider = Physics2D.OverlapCircle(is_ground_checker.position, check_ground_radius, ground_layer);
        if (collider != null) {
            is_grounded = true;
        }
        else {
            if (is_grounded) {
                last_time_grounded = Time.time;
            }
            is_grounded = false;
        }
    }
}
