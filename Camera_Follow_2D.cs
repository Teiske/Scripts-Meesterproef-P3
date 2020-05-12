using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow_2D : MonoBehaviour {

    [SerializeField] private GameObject follow_object;
    [SerializeField] private Vector2 follow_offset;
    [SerializeField] private float camera_speed;

    private Rigidbody2D rigidbody_2D;
    private Vector2 threshold;

    // Start is called before the first frame update
    void Start() {
        threshold = CalculateThreshold();
        rigidbody_2D = follow_object.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    // FixedUpdate is called at a fixed time interval
    void FixedUpdate() {
        Vector2 follow = follow_object.transform.position;
        // x_difference keeps track of how far the character is away from the center of the X axis
        float x_difference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        // y_difference keeps track of how far the character is away from the center of the Y axis
        float y_difference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        // Calculate the new camera position
        Vector3 new_position = transform.position;
        if (Mathf.Abs(x_difference) >= threshold.x) {
            new_position.x = follow.x;
        }
        if (Mathf.Abs(y_difference) >= threshold.y) {
            new_position.y = follow.y;
        }
        float move_speed = rigidbody_2D.velocity.magnitude > camera_speed ? rigidbody_2D.velocity.magnitude : camera_speed;
        transform.position = Vector3.MoveTowards(transform.position, new_position, move_speed * Time.deltaTime);
    }

    // CalculateThreshold is used to see when the camera needs to start following the player
    private Vector3 CalculateThreshold() {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= follow_offset.x;
        t.y -= follow_offset.y;

        return t;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
