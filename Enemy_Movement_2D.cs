using UnityEngine;

public class Enemy_Movement_2D : MonoBehaviour {

    [SerializeField] private float move_speed;
    [SerializeField] private float ray_distance;
    [SerializeField] private Transform ground_detection;

    private bool moving_right = true;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    // FixedUpdate is called at a fixed time interval
    void FixedUpdate() {
        transform.Translate(Vector2.right * move_speed * Time.deltaTime);
        RaycastHit2D ground_info = Physics2D.Raycast(ground_detection.position, Vector2.down, ray_distance);
        if (ground_info.collider == false) {
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
