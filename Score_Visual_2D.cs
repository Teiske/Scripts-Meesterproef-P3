using UnityEngine;
using UnityEngine.UI;

public class Score_Visual_2D : MonoBehaviour {

    Score_System_2D score_System_2D;

    [SerializeField] public Text score_text;

    // Awake is called when the script instance is being loaded.
    private void Awake() {
        score_System_2D = GameObject.Find("Gamemanager").GetComponent<Score_System_2D>();
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        score_text.text = "Score: " + score_System_2D.Score_Value;
    }
}
