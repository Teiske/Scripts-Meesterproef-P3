using UnityEngine;

public class Health_System_2D : MonoBehaviour {

    [SerializeField] private int max_health;
    [SerializeField] private int current_health;
    
    public int Current_Health {
        get { return current_health; }
        set { current_health = value; }
    }

    // Start is called before the first frame update
    private void Start() {
        current_health = max_health;
    }

    // Update is called once per frame
    private void Update() {
        
    }

    // DamagePlayer is called when the enemy deals damage to the player
    public void DamagePlayer(int damage) {
        current_health -= damage;
    }
}
