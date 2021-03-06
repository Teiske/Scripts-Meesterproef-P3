﻿using UnityEngine;
using UnityEngine.UI;

public class Score_Visual_2D : MonoBehaviour {

    Score_System_2D score_system_2D;

    [SerializeField] public Text score_text;

    // Awake is called when the script instance is being loaded.
    private void Awake() {
        score_system_2D = GameObject.Find("Gamemanager").GetComponent<Score_System_2D>();
    }

    // Update is called once per frame
    void Update() {
        score_text.text = "Score: " + score_system_2D.Score_Value;
    }
}
