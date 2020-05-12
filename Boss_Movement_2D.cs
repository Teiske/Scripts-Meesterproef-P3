using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Boss_Movement_2D : MonoBehaviour {

    private Player_Movement_2D player_Movement_2D;

    [SerializeField] private int boss_health;
    [SerializeField] private int damage_to_player;
    private float time_btw_damage = 1.5f;

    [SerializeField] private Slider health_bar;
    [SerializeField] private bool is_dead;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
       
        if (boss_health <= 0) {
            anim.SetTrigger("death");
        }

        // give the player some time to recover before taking more damage !
        if (time_btw_damage > 0) {
            time_btw_damage -= Time.deltaTime;
        }

        health_bar.value = boss_health;
    }

    private void OnCollisionEnter2D(Collision2D collision_2D) {
        player_Movement_2D = collision_2D.collider.GetComponent<Player_Movement_2D>();

        // deal the player damage ! 
        if (collision_2D.gameObject.tag == "Player" && is_dead == false) {
            if (time_btw_damage <= 0) {
                FindObjectOfType<Health_System_2D>().DamagePlayer(damage_to_player);
            }
        }

        if (collision_2D.gameObject.tag == "Invisible_Wall") {
            Physics2D.IgnoreCollision(collision_2D.collider, GetComponent<Collider2D>());
        }
    }
}
