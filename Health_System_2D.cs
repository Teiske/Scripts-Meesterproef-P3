using UnityEngine;

public class Health_System_2D : MonoBehaviour {

    [SerializeField] private int max_health;
    [SerializeField] private int current_health;

    // Start is called before the first frame update
    void Start() {
        current_health = max_health;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void DamagePlayer(int damage) {
        current_health -= damage;
    }
}
