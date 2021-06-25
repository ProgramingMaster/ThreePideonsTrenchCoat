using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpener : MonoBehaviour
{   
    private bool atDoor;

    void Start() {
        atDoor = false;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D (Collider2D collider) {
        atDoor = true;
    }
    
    private void OnTriggerExit2D (Collider2D collider) {
        atDoor = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (atDoor) {
            if (Input.GetKeyDown("g")) {
                SceneManager.LoadScene("Backery");
            }
        }
    }
}
