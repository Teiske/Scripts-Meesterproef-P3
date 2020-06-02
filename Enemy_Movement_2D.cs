using UnityEngine;

public class Enemy_Movement_2D : MonoBehaviour {

    Score_System_2D score_system_2D;

    [SerializeField] private float move_speed;
    [SerializeField] private float ray_distance;
    [SerializeField] private int damage_to_player;
    [SerializeField] private float damage_delay;
    [SerializeField] private float damage_timer;
    [SerializeField] private Transform wall_detection;

    private bool moving_right = true;

    private bool damage_dealt = false;

    // Awake is called when the script instance is being loaded.
    private void Awake() {
        score_system_2D = GameObject.Find("Gamemanager").GetComponent<Score_System_2D>();
    }

    // Start is called before the first frame update
    void Start() {
       
    }

    // Update is called once per frame
    void Update() {
        // Cast out a ray infront of the enemy
        RaycastHit2D hit_2D = Physics2D.Raycast(wall_detection.position, Vector2.left, ray_distance);

        // If the ray hits an invisible wall, make the enemy walk to the left and vice versa
        if (hit_2D.collider.CompareTag("Invisible_Wall")) {
            EnemyFlip();
            Debug.Log(hit_2D + " hit the wall");
        }

        // If the hits the player, deal damage to the player
        if (hit_2D.collider.CompareTag("Player") && damage_dealt == false) {
            Debug.Log(hit_2D + " hit the player");
            FindObjectOfType<Health_System_2D>().DamagePlayer(damage_to_player);
            damage_dealt = true;
            EnemyFlip();
        }

        // If damage has been dealt, wait a few seconds before dealing damage again
        if (damage_dealt == true) {
            damage_timer += Time.deltaTime;
            if (damage_timer >= damage_delay) {
                damage_timer = 0f;
                damage_dealt = false;
            }
        }
    }

    // FixedUpdate is called at a fixed time interval
    void FixedUpdate() {
        // Make the enemy move to the right
        transform.Translate(Vector2.left * move_speed * Time.deltaTime);
    }

    private void EnemyFlip() {
        if (moving_right == true) {
            transform.eulerAngles = new Vector3(0, -180, 0);
            moving_right = false;
        }
        else {
            transform.eulerAngles = new Vector3(0, 0, 0);
            moving_right = true;
        }
    }

    // EnemyDeath is called when the player jumps on the enemy
    public void EnemyDeath() {
        Destroy(gameObject);
    }
}
