using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_System_2D : MonoBehaviour {

    [SerializeField] private Canvas quit_menu;

    [SerializeField] private Button start_button;
    [SerializeField] private Button exit_button;

    // Start is called before the first frame update
    void Start() {
        quit_menu = quit_menu.GetComponent<Canvas>();
        start_button = start_button.GetComponent<Button>();
        exit_button = exit_button.GetComponent<Button>();

        quit_menu.enabled = false;
    }

    public void ExitPress() {
        quit_menu.enabled = true;
        start_button.enabled = false;
        exit_button.enabled = false;
    }

    public void NoPress() {
        quit_menu.enabled = false;
        start_button.enabled = true;
        exit_button.enabled = true;
    }

    public void StartLevel() {
        SceneManager.LoadScene(1);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
