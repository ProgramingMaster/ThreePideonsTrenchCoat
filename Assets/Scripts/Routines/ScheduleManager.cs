using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class ScheduleManager : MonoBehaviour
{
    public bool StartInScene;
    public GameObject sayImage;
    private TMP_Text sayText;
    public Animator anim;

    public string actionType;

    private Schedule schedule;
    private Action[] actions;
    private Vector2 TheBadPlace;
    private bool inScene;
    //Transform transform;
    int i;
    float startPos;
    float endPos;
    float step;
    int duration;
    int numberOfLettersToAdd;
    SpriteRenderer renderer;

    [@System.NonSerialized]
    public Conversation conversation;

    // Start is called before the first frame update
    void Start()
    {
        numberOfLettersToAdd = 0;
        //transform = GetComponent<Transform>();
        //walking = false;
        i = 0;
        TheBadPlace = new Vector2(-1000, -1000);
        inScene = StartInScene;
        //StartAction();
        sayText = sayImage.GetComponentInChildren<TMP_Text>();
        sayImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    { // actions = schedule
                //Debug.Log("Duration" + duration);
        //Debug.Log(actions);
        if (i < actions.Length) {
            //Debug.Log(i);
            if (actions[i].anim != null) {
                anim.Play(actions[i].anim);
            }
        }
    }

    // void FixedUpdate() {
    //     if (walking && inScene) {
    //         //super easy, barely an inconvience
    //         float step = (Mathf.Abs(endPos - startPos) / actions[i].timeslot.duration) * Time.deltaTime;

    //         transform.position = Vector2.MoveTowards(transform.position, actions[i].endPosition, step);
    //         //Debug.Log(actions[i].endPosition);
    //     }
    // }

    public void StartAction() {
        renderer = GetComponent<SpriteRenderer>();
        schedule = GetComponent<CharacterManager>().schedule;
        Debug.Log("StartAction: " + schedule.schedule);
        actions = schedule.schedule;
        StartCoroutine(ActionManager());
    }

    IEnumerator ActionManager() {
        int time = (GameManager.Instance.gameTimeHour * 60) + GameManager.Instance.gameTimeMinute;
        int startTime;

        for (i = 0; i < actions.Length; i++) {
            Debug.Log("Iteration: " + i);
            actionType = actions[i].type;
            renderer.sortingLayerName = "Layer" + actions[i].layer;

            if (actions[i].scene == SceneManager.GetActiveScene().name) {
                inScene = true;
            }

            if (inScene) 
                transform.position = new Vector2(actions[i].startPosition.x, actions[i].startPosition.y);

            // Makes a single number for the start time
            startTime = (actions[i].timeslot.startTime.hour * 60) + actions[i].timeslot.startTime.minute;
            Debug.Log("Time" + time + " & " + startTime);
            // if the current time is greater than the time the action was suppose to start
            if (time > startTime) {
                Debug.Log("Welp");
                // Then if the current time is still within the duration that the action was suppose to start
                if (time < startTime + actions[i].timeslot.duration ) {
                    Debug.Log("Yay!");
                    // Set their current position and the new duration (how long it should take them to finish the action now)
                    if (actions[i].type == "WalkX") {
                        // startPos = actions[i].startPosition.x;
                        // endPos = actions[i].endPosition.x;
                        Debug.Log("Walking!");
                        duration = (startTime + actions[i].timeslot.duration) - time;

                        if (inScene) {
                            float step = (Mathf.Abs(actions[i].endPosition.x - actions[i].startPosition.x) / actions[i].timeslot.duration);
                            transform.position = new Vector3((actions[i].startPosition.x - step * (time - startTime)), transform.position.y, transform.position.z);
                            Debug.Log(actions[i].startPosition.x);
                            Debug.Log((actions[i].startPosition.x - step * (time - startTime)));
                        }
                    } else if (actions[i].type == "WalkY") {
                        // startPos = actions[i].startPosition.y;
                        // endPos = actions[i].endPosition.y;
                        duration = (startTime + actions[i].timeslot.duration) - time;
                        if (inScene) {
                            float step = (Mathf.Abs(actions[i].endPosition.y - actions[i].startPosition.y) / actions[i].timeslot.duration);
                            transform.position = new Vector3(transform.position.x, (actions[i].startPosition.y + step * (time - startTime)), transform.position.z);
                        }
                    } else if (actions[i].type == "Say") {
                        duration = (startTime + actions[i].timeslot.duration) - time;
                        //Debug.Log("Duration: " + ((startTime + actions[i].timeslot.duration) - time) );
                        char[] sayArray = actions[i].say.ToCharArray();
                        float typingSpeed = (float) ((float) actions[i].timeslot.duration/ (float) sayArray.Length);
                        numberOfLettersToAdd = (int) ((actions[i].timeslot.duration - duration) / typingSpeed);
                        Debug.Log("Info: " + i + " " + typingSpeed + " " + duration + " " + numberOfLettersToAdd + " " + actions[i].timeslot.duration + " " + (duration/sayArray.Length) + " " + time + " " + startTime);
                        sayText.text = actions[i].say.Substring(0, numberOfLettersToAdd); 
                    }
                    else {
                        duration = (startTime + actions[i].timeslot.duration) - time;
                    }
                }
                else {
                    Debug.Log("Continue");
                    continue;
                }
            }
            // If you /are/ on time then set the duration as normal
            else {
                Debug.Log("time == starttime");
                duration = actions[i].timeslot.duration;
                if (inScene)
                    transform.position = new Vector2(actions[i].startPosition.x, actions[i].startPosition.y);
                Debug.Log("Time == StartPosition: " + actions[i].startPosition);
            }
            
            if (actions[i].scene != SceneManager.GetActiveScene().name) {
                transform.position = TheBadPlace;
                inScene = false;
            }

            Debug.Log("153: " + actions[i].layer );
            conversation = actions[i].startDialogue;
            Debug.Log("155: " + conversation);

            string type = actions[i].type;
            if (type == "Idle") {
                yield return StartCoroutine(Idle());
            } else if (type == "WalkX" || type == "WalkY") {
                yield return StartCoroutine(Walk());
            } else if (type == "SwitchScene") {
                //SceneManager.LoadScene(actions[i].scene);
                Debug.Log("Switch!");
            } else if (type == "Say") {
                yield return StartCoroutine(Say());
            } 
        }
        if (i >= actions.Length) {
            Debug.Log("End");
        }
    }

    IEnumerator Say() {
        sayImage.SetActive(true);
        char[] sayArray = (actions[i].say.Substring((numberOfLettersToAdd), (actions[i].say.Length - (numberOfLettersToAdd)))).ToCharArray();
        //Debug.Log((actions[i].say.Substring((numberOfLettersToAdd -1), actions[i].say.Length)));
        float typingSpeed = (float)( (duration) / sayArray.Length);
        Debug.Log("typing speed: " + typingSpeed);
        foreach(char letter in sayArray) {
            sayText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        // yield return new WaitForSecondsRealtime((float)(duration * 0.1));
        sayImage.SetActive(false);
        numberOfLettersToAdd = 0;
    }

    //Need to make function to determine walk speed based on duration
    // IEnumerator Walk() {
    //     walking = true;
    //     if (actions[i].type == "WalkX") {
    //         startPos = actions[i].startPosition.x;
    //         endPos = actions[i].endPosition.x;
    //     } else if (actions[i].type == "WalkY") {
    //         startPos = actions[i].startPosition.y;
    //         endPos = actions[i].endPosition.y;
    //     }
    //     Debug.Log("StartPos: " + startPos + " EndPos: " + endPos);
    //     yield return new WaitForSecondsRealtime(duration);
    //     walking = false;
    // }

    public IEnumerator Walk() {
        float t = 0f;
        while(t < 1) {
                t += Time.deltaTime / duration;
                transform.position = Vector2.Lerp(actions[i].startPosition, actions[i].endPosition, t);
                yield return null;
        }
    }

    IEnumerator Idle() {
       transform.position = new Vector2(actions[i].startPosition.x, actions[i].startPosition.y);
       yield return new WaitForSecondsRealtime(duration);
    }
}
