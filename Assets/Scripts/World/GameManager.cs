using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{

    [Header("Events")]
	[Space]

	public UnityEvent Loaded;
    public static GameManager Instance { get; set; }

    //public SaveSystem Save;
    public float playerHealth=100;
    public int gameTimeHour = 2;
    public int gameTimeMinute = 0;
    public bool inTrenchcoat;

    public Follower follower1;
    public Follower follower2;

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
        if (Loaded == null)
            Loaded = new UnityEvent();
        GameLoad();
    }

    public void NewGame() {
        ES3.DeleteKey("schedules");
        ES3.DeleteKey("conditions");
        ES3.DeleteKey("follower1");
        ES3.DeleteKey("follower2");
        ES3.DeleteKey("timeHour");
        ES3.DeleteKey("timeMinute");
        SceneManager.LoadScene("Theater");
    }

    public void GameLoad() {
        if (ES3.KeyExists("conditions"))
            conditions = ES3.Load<Dictionary<string, bool>>("conditions");
        if (ES3.KeyExists("schedules")) {
            Debug.Log("Schedule Exists");
            schedules = ES3.Load<Dictionary<string, Schedule>>("schedules");
        }
        if (ES3.KeyExists("follower1") && ES3.KeyExists("follower2")) {
            // follower1 = new Follower();
            // follower2 = new Follower();
            follower1 = ES3.Load<Follower>("follower1");
            Debug.Log(follower1 == null);
            follower2 = ES3.Load<Follower>("follower2");
            Debug.Log(follower2 == null);
            //Debug.Log("Saved" + follower1.name + " & " + follower2.name);
        }

        if (ES3.KeyExists("timeHour") && ES3.KeyExists("timeMinute")) {
            Debug.Log("Tiiiiime!");
            gameTimeHour = ES3.Load<int>("timeHour");
            gameTimeMinute = ES3.Load<int>("timeMinute");
        }
        Loaded.Invoke();
    }

    public void GameSave() {
        ES3.Save("conditions", conditions);
        ES3.Save("schedules", schedules);
        //Debug.Log("Saving: " + follower1.name + " & " + follower2.name);
        //Debug.Log("")
        ES3.Save("follower1", follower1);
        ES3.Save("follower2", follower2);
        ES3.Save("timeHour", gameTimeHour);
        ES3.Save("timeMinute", gameTimeMinute);
    }

    public void Summon(Follower follower, GameObject slot, Vector2 position) {
        Debug.Log("Summoned!");

        GameObject character = GameObject.Find("Characters/" + follower.name);
        if (character != null)
            character.SetActive(false);

        SpriteRenderer slotSprite = slot.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        Animator slotAnimator = slot.GetComponent(typeof(Animator)) as Animator;
        slotSprite.sprite = follower.sprite;
        slotAnimator.runtimeAnimatorController = follower.anim;
        slot.transform.position = position;
    }
}
