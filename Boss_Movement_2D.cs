using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Movement_2D : MonoBehaviour { 

    [SerializeField] private int boss_health;
    [SerializeField] private int damage_to_player;

    [SerializeField] private float boss_speed;
    [SerializeField] private float time_btw_damage;
    [SerializeField] private float damage_timer;
    [SerializeField] private float ray_distance;

    [SerializeField] private Transform player_detection;
    [SerializeField] private Transform ray_damage;

    [SerializeField] private Slider health_bar;

    private Animator boss_animator;

    private bool is_dead = false;
    private bool can_move = true;
    private bool moving_right = false;

    // Start is called before the first frame update
    void Start() {
        boss_animator = GetComponent<Animator>();
        player_detection = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        damage_timer = time_btw_damage;
    }

    // Update is called once per frame
    void Update() {

        if (boss_health <= 0) {
            boss_animator.SetTrigger("Boss_Death");
            is_dead = true;
        }

        // give the player some time to recover before taking more damage !
        if (damage_timer > 0) {
            damage_timer -= Time.deltaTime;
        }

        health_bar.value = boss_health;

        if (transform.position.x < player_detection.position.x && !moving_right) {
            BossFlip();
        }
        else if (transform.position.x > player_detection.position.x && moving_right) {
            BossFlip();
        }
    }

    // FixedUpdate is called at a fixed time interval
    void FixedUpdate() {
        MoveBoss();
        DamagePlayer();
    }

    // Move the boss character towards the player
    void MoveBoss() {
        if (can_move == true) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player_detection.position.x, transform.position.y), boss_speed * Time.deltaTime);
            boss_animator.SetFloat("Boss_Speed", Mathf.Abs(boss_speed));
        }
    }

    // Make the boss face towards the player
    void BossFlip() {
        moving_right = !moving_right;
        Vector3 boss_scale = gameObject.transform.localScale;
        boss_scale.x *= -1;
        gameObject.transform.localScale = boss_scale;
    }

    // Damage the player when it comes in to contact with the ray the boss shoots forward
    void DamagePlayer() {
        RaycastHit2D enemy_info = Physics2D.Raycast(ray_damage.position, Vector2.left, ray_distance);
        Debug.DrawRay(ray_damage.position, Vector2.left, Color.green);

        if (damage_timer <= 0) {
            if (enemy_info.transform.gameObject.CompareTag("Player") && is_dead == false) {
                FindObjectOfType<Health_System_2D>().DamagePlayer(damage_to_player);
                damage_timer = time_btw_damage;
            }
        }
    }

    // DamageBoss is called when the player jumps on the boss
    public void DamageBoss() {
        boss_health -= 1;
        boss_animator.SetTrigger("Boss_Hurt");
        can_move = false;
        if (can_move == false) {
            StartCoroutine(WaitToMove());
        }
        Debug.Log(boss_health);
    }

    private void OnCollisionEnter2D(Collision2D collision_2D) {
        if (collision_2D.gameObject.CompareTag("Platform")) {
            Physics2D.IgnoreCollision(collision_2D.collider, GetComponent<Collider2D>());
        }
    }

    // Makes the boss stop moving for one second after being hurt by the player
    IEnumerator WaitToMove() {
        yield return new WaitForSeconds(1.0f);
        can_move = true;
    }
}
