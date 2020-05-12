using UnityEngine;

public class Score_System_2D : MonoBehaviour {

    [SerializeField] private static int score_value = 0;
    private int enemy_value = 10;
    
    public int Score_Value {
        get { return score_value; }
        set { score_value = value; }
    }

    // Start is called before the first frame update
    void Start() {
      
    }

    // Update is called once per frame
    void Update() {
        
    }

    // EnemyDeath is called when the player jumps on the enemy
    public void EnemyScore() {
        score_value += enemy_value;
        Debug.Log(score_value);
    }
}
