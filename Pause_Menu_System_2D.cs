using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause_Menu_System_2D : MonoBehaviour {

    [SerializeField] private Canvas pause_menu;

    [SerializeField] private Button Continue_button;
    [SerializeField] private Button quitToMenu_button;
    [SerializeField] private Button exit_button;

    // Start is called before the first frame update
    void Start() {
        
        pause_menu = pause_menu.GetComponent<Canvas>();
       
        Continue_button = Continue_button.GetComponent<Button>();
        quitToMenu_button = quitToMenu_button.GetComponent<Button>();
        exit_button = exit_button.GetComponent<Button>();

        pause_menu.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pause_menu.enabled = !pause_menu.enabled;
        }
    }

    public void StartLevel() {
        SceneManager.LoadScene(1);
    }

    public void QuitToMenuPress() {

        SceneManager.LoadScene(0);

    }

    public void ContinuePress() {

        pause_menu.enabled = false;

    }

    public void ExitGame() {
        Application.Quit();
    }
}
