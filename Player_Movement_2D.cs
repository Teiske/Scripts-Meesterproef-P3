using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player_Movement_2D : MonoBehaviour {

    private Health_System_2D health_system_2D;
    private Enemy_Movement_2D enemy_movement_2D;
    private Score_System_2D score_system_2D;

    private Rigidbody2D rigidBody2D;
    private Animator animator2D;

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

    private int layer_invisble_wall;

    private bool is_facing_right = true;
    private bool is_jumping = false;

    // Awake is called when the script instance is being loaded.
    private void Awake() {
        health_system_2D = GameObject.Find("Gamemanager").GetComponent<Health_System_2D>();
        score_system_2D = GameObject.Find("Gamemanager").GetComponent<Score_System_2D>();
    }

    // Start is called before the first frame update
    void Start() {
        rigidBody2D = this.gameObject.GetComponent<Rigidbody2D>();
        animator2D = GetComponent<Animator>();

        layer_invisble_wall = LayerMask.NameToLayer("Invisible_Wall");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump")) {
            is_jumping = true;
        }
        if (is_jumping == true) {
            animator2D.SetBool("is_jumping", true);
        }
    }

    // FixedUpdate is called at a fixed time interval
    void FixedUpdate() {
        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
    }

    // Move is called when either A/D or the left/right arrow keys are pressed
    private void Move() {
        float x_movement = Input.GetAxisRaw("Horizontal");
        float move_by = x_movement * max_speed;
        rigidBody2D.velocity = new Vector2(move_by, rigidBody2D.velocity.y);
        animator2D.SetFloat("max_speed", Mathf.Abs(move_by));

        if (move_by > 0 && !is_facing_right) {
            Flip();
        }
        else if (move_by < 0 && is_facing_right) {
            Flip();
        }
    }

    // Jump is called when the space key is pressed
    private void Jump() {
        if (Input.GetKey(KeyCode.Space) && (is_grounded || Time.time - last_time_grounded <= remember_grounded_for)) {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jump_Force);
        }
    }

    // BetterJump is called after jump to smooth the jump in the air and the fall back down
    private void BetterJump() {
        if (rigidBody2D.velocity.y < 0) {
            rigidBody2D.velocity += Vector2.up * Physics2D.gravity * (fall_multiplier - 1) * Time.deltaTime;
        }
        else if (rigidBody2D.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rigidBody2D.velocity += Vector2.up * Physics2D.gravity * (low_jump_multiplier - 1) * Time.deltaTime;
        }
    }

    // CheckIfGrounded is called at a fixed interval in FixedUpdate to check if the player is grounded
    private void CheckIfGrounded() {
        Collider2D collider = Physics2D.OverlapCircle(is_ground_checker.position, check_ground_radius, ground_layer);
        if (collider != null) {
            is_grounded = true;
            is_jumping = false;
            if (is_jumping == false) {
                animator2D.SetBool("is_jumping", false);
            }
        }
        else {
            if (is_grounded) {
                last_time_grounded = Time.time;
            }
            is_grounded = false;
        }
    }

    // Flip is called when the player changes directions
    private void Flip() {
        is_facing_right = !is_facing_right;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision_2D) {
        enemy_movement_2D = collision_2D.collider.GetComponent<Enemy_Movement_2D>();

        if (enemy_movement_2D != null) {
            foreach (ContactPoint2D point2D in collision_2D.contacts) {
                if (point2D.normal.y >= 0.9f) {
                    gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 125f);
                    enemy_movement_2D.EnemyDeath();
                    score_system_2D.EnemyScore();
                }
            }
        }

        if (collision_2D.gameObject.tag == "Invisible_Wall") {
            Physics2D.IgnoreCollision(collision_2D.collider, GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger_2D) {
        score_system_2D.Score_Value += 10;
    }
}
