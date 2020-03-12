using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy_Movement_2D : MonoBehaviour {

    [SerializeField] private float move_speed;
    [SerializeField] private float ray_distance;
    [SerializeField] private int damage_to_player;
    [SerializeField] private float damage_delay;
    [SerializeField] private float damage_timer;
    [SerializeField] private Transform wall_detection;
    [SerializeField] private Transform weak_point;

    private bool moving_right = true;

    private bool damage_dealt = false;

    private int layer_invisble_wall;

    // Start is called before the first frame update
    void Start() {
        layer_invisble_wall = LayerMask.NameToLayer("Invisible_Wall");
    }

    // Update is called once per frame
    void Update() {
        // If damage has been dealt, wait a few seconds before dealing damage again
        if(damage_dealt == true) {
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
        transform.Translate(Vector2.right * move_speed * Time.deltaTime);

        // Cast out a ray infront of the enemy
        RaycastHit2D wall_info = Physics2D.Raycast(wall_detection.position, Vector2.right, ray_distance);

        // If the ray hits an invisible wall, make the enemy walk to the left and vice versa
        if (wall_info.transform.gameObject.layer == layer_invisble_wall) {
            if (moving_right == true) {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moving_right = false;
            }
            else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moving_right = true;
            }
        }

        // If the hits the player, deal damage to the player
        if (wall_info.transform.gameObject.tag == "Player" && damage_dealt == false) {
            FindObjectOfType<Health_System_2D>().DamagePlayer(damage_to_player);
            damage_dealt = true;
        }
    }

    public void EnemyDeath() {
        Destroy(gameObject, 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision_2D) {
        //if (collision_2D.gameObject.tag == "Player") {
        //    float height = collision_2D.contacts[0].point.y - weak_point.position.y;
        //    if (height > 0) {
        //        Destroy(gameObject);
        //    }
        //}
    }
}
