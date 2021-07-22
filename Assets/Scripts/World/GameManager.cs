using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    //public SaveSystem Save;
    public float playerHealth=100;
    public int gameTimeHour = 2;
    public int gameTimeMinute = 0;
    public bool inTrenchcoat;


    public Dictionary<string, bool> conditions = new Dictionary<string, bool> {
        //Stuff
        //Test
        {"AskedToggyForPassword", false},
        {"AgreedToGetPasswordForFlower", false},
        //Theater
        {"WazuFollower", false},
        {"FluffFollower", false},
        {"HasTrenchcoat", false},
        {"LookingForTrenchCoat", false},
    };

    public Dictionary<string, Schedule> schedules = new Dictionary<string, Schedule> {};

    private void Awake()
    {  
        //Debug.Log("Wazu: " + conditions["Wazu"]);
        Instance = this;
    }

    private void Start() {
        GameLoad();
    }

    public void InTrenchcoat() {
        inTrenchcoat = true;
    }

    public void NotInTrenchcoat() {
        inTrenchcoat = false;
    }

    public void NewGame() {
        ES3.DeleteKey("schedules");
        ES3.DeleteKey("conditions");
        SceneManager.LoadScene("Theater");
    }

    public void GameLoad() {
        if (ES3.KeyExists("conditions"))
            conditions = ES3.Load<Dictionary<string, bool>>("conditions");
        if (ES3.KeyExists("schedules")) {
            Debug.Log("Schedule Exists");
            schedules = ES3.Load<Dictionary<string, Schedule>>("schedules");
        }
    }

    public void GameSave() {
        ES3.Save("conditions", conditions);
        ES3.Save("schedules", schedules);
    }
}
