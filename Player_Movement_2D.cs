using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement_2D : MonoBehaviour {

    private Health_System_2D health_system_2D;
    private Enemy_Movement_2D enemy_movement_2D;
    private Boss_Movement_2D boss_Movement_2D;
    private Score_System_2D score_system_2D;

    private Rigidbody2D rigidBody2D;
    private Animator animator2D;

    [SerializeField] private float max_speed;
    [SerializeField] private float jump_Force;
    [SerializeField] private float groundend_skin;
    [SerializeField] private float fall_multiplier;
    [SerializeField] private float low_jump_multiplier;

    [SerializeField] private LayerMask layer_mask;

    private bool is_facing_right = true;
    private bool is_jumping;
    private bool is_grounded;

    private Vector2 player_size;
    private Vector2 box_size;

    // Awake is called when the script instance is being loaded.
    private void Awake() {
        health_system_2D = GameObject.Find("Gamemanager").GetComponent<Health_System_2D>();
        score_system_2D = GameObject.Find("Gamemanager").GetComponent<Score_System_2D>();

        player_size = GetComponent<BoxCollider2D>().size;
        box_size = new Vector2(player_size.x, groundend_skin);
    }

    // Start is called before the first frame update
    void Start() {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator2D = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump") && is_grounded) {
            is_jumping = true;
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
        if (is_jumping == true) {
            rigidBody2D.AddForce(Vector2.up * jump_Force, ForceMode2D.Impulse);
            animator2D.SetTrigger("is_jumping");
            is_jumping = false;
            is_grounded = false;
        }
    }

    // BetterJump is called after jump to smooth the jump in the air and the fall back down
    private void BetterJump() {
        if (rigidBody2D.velocity.y < 0) {
            rigidBody2D.gravityScale = fall_multiplier;
            animator2D.SetBool("is_jumping", false);
            animator2D.SetBool("is_falling", true);
        }
        else if (rigidBody2D.velocity.y > 0 && !Input.GetButton("Jump")) {
            rigidBody2D.gravityScale = low_jump_multiplier;
        }
        else {
            rigidBody2D.gravityScale = 1f;
        }
    }

    // CheckIfGrounded is called at a fixed interval in FixedUpdate to check if the player is grounded
    private void CheckIfGrounded() {
        Vector2 box_center = (Vector2)transform.position + Vector2.down * (player_size.y + box_size.y) * 0.5f;
        is_grounded = (Physics2D.OverlapBox(box_center, box_size, 0f, layer_mask) != null);
        if (is_grounded) {
            animator2D.SetBool("is_falling", false);
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
        boss_Movement_2D = collision_2D.collider.GetComponent<Boss_Movement_2D>();

        if (enemy_movement_2D != null) {
            if (collision_2D.collider.GetType() == typeof(CapsuleCollider2D)) {
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 187.5f);
                score_system_2D.EnemyScore();
                enemy_movement_2D.EnemyDeath();
            }
        }

        if (boss_Movement_2D != null) {
            if (collision_2D.collider.GetType() == typeof(CapsuleCollider2D)) {
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 187.5f);
                boss_Movement_2D.DamageBoss();
            }
        }

        if (collision_2D.gameObject.tag == "Invisible_Wall") {
            Physics2D.IgnoreCollision(collision_2D.collider, GetComponent<Collider2D>());
        }

        if (collision_2D.gameObject.tag == "End_Level") {
            SceneManager.LoadScene(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger_2D) {
        score_system_2D.Score_Value += 10;
    }
}
