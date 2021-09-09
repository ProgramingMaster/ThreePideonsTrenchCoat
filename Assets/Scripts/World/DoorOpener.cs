using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpener : MonoBehaviour
{   
    private bool atDoor;
    public string scene;
    public Vector2 newPos;
    //public SaveSystem Save;

    void Start() {
        atDoor = false;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D (Collider2D collider) {
        Debug.Log("At Door");
        atDoor = true;
    }
    
    private void OnTriggerExit2D (Collider2D collider) {
        if (collider.isTrigger == true && collider.GetType() != typeof(BoxCollider2D)) {
            Debug.Log("Left Door");
            atDoor = false;
        }  
    }


    // Update is called once per frame
    void Update()
    {
        if (atDoor) {
            //Debug.Log("AT door");
            if (Input.GetKeyDown("g")) {
                Debug.Log("g");
                GameManager.Instance.position = new Vector2(newPos.x, newPos.y);
                GameManager.Instance.GameSave();
                SceneManager.LoadScene(scene);
            }
        }
    }
}
