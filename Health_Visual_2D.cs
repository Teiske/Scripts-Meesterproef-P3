using UnityEngine;
using UnityEngine.UI;

public class Health_Visual_2D : MonoBehaviour {

    Health_System_2D health_system_2D;

    [SerializeField] private int num_of_health;

    [SerializeField] private Image[] health_image;
    [SerializeField] private Sprite health_sprite_full;
    [SerializeField] private Sprite health_sprite_empty;

    // Awake is called when the script instance is being loaded.
    private void Awake() {
        health_system_2D = GameObject.Find("Gamemanager").GetComponent<Health_System_2D>();
    }

    // Update is called once per frame
    private void Update() {
        // Set the health images equal to the player current health
        if (health_system_2D.Current_Health > num_of_health) {
            health_system_2D.Current_Health = num_of_health;
        }
        for (int i = 0; i < health_image.Length; i++) {
            if (i < health_system_2D.Current_Health) {
                health_image[i].sprite = health_sprite_full;
            }
            else {
                health_image[i].sprite = health_sprite_empty;
            }
            if (i < num_of_health) {
                health_image[i].enabled = true;
            }
            else {
                health_image[i].enabled = false;
            }
        }
    }
}
