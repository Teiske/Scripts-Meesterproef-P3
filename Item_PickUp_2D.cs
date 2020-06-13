using UnityEngine;

public class Item_PickUp_2D : MonoBehaviour {

    Score_System_2D score_system_2D;

    private void OnTriggerEnter2D(Collider2D trigger_2D) {
        if (trigger_2D.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            score_system_2D.Score_Value += 5;
        }
    }
}
