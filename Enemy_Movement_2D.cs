using UnityEngine;

public class Enemy_Movement_2D : MonoBehaviour {

    [SerializeField] private float move_speed;
    [SerializeField] private float ray_distance;
    [SerializeField] private Transform wall_detection;

    private bool moving_right = true;

    private int layer_invisble_wall;

    // Start is called before the first frame update
    void Start() {
        layer_invisble_wall = LayerMask.NameToLayer("Invisible_Wall");
    }

    // Update is called once per frame
    void Update() {
        
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
    }
}
