using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    bool GamePaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            if (GamePaused)
                Play();
            else
                Pause();
        }
    }

    private void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Play() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
}
