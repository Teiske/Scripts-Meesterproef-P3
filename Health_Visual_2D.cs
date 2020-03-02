using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Visual_2D : MonoBehaviour {

    [SerializeField] private Sprite health_sprite;

    private List<Health_Image> health_image_list;

    // Awake is called when the script instance is being loaded.
    private void Awake() {
        health_image_list = new List<Health_Image>();
    }

    // Start is called before the first frame update
    private void Start() {
        CreateHealthImage(new Vector2(0, 0));
        CreateHealthImage(new Vector2(25, 0));
        CreateHealthImage(new Vector2(50, 0));
    }

    // Update is called once per frame
    private void Update() {
        
    }

    // Create an image on the canvas
    private Image CreateHealthImage(Vector2 anchord_position) {
        // Create game object
        GameObject health_gameobject = new GameObject("Health", typeof(Image));

        // Set as child of this transform
        health_gameobject.transform.parent = transform;
        health_gameobject.transform.localPosition = Vector3.zero;

        // Locate and size image
        health_gameobject.GetComponent<RectTransform>().anchoredPosition = anchord_position;
        health_gameobject.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);

        // Set image sprite
        Image health_image_UI = health_gameobject.GetComponent<Image>();
        health_image_UI.sprite = health_sprite;

        Health_Image health_Image = new Health_Image(health_image_UI);
        health_image_list.Add(health_Image);

        return health_image_UI;
    }

    public class Health_Image {

        private Image health_image;

        public Health_Image(Image healht_image) {
            health_image = health_image;
        }
    }
}
