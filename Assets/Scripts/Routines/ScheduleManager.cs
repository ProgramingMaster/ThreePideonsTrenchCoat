using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleManager : MonoBehaviour
{
    private Schedule schedule;
    private Action[] actions;
    //Transform transform;
    bool walking;
    int i;
    float startPos;
    float endPos;
    float step;
    int duration;
    [@System.NonSerialized]
    public Conversation conversation;

    // Start is called before the first frame update
    void Start()
    {
        //transform = GetComponent<Transform>();
        walking = false;
        i = 0;
        //StartAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (walking) {
            //super easy, barely an inconvience
            float step = (Mathf.Abs(endPos - startPos) / actions[i].timeslot.duration) * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, actions[i].endPosition, step);
            //Debug.Log(actions[i].endPosition);
        }
    }

    public void StartAction() {
        schedule = GetComponent<CharacterManager>().schedule;
        Debug.Log("StartAction: " + schedule.schedule);
        actions = schedule.schedule;
        StartCoroutine(ActionManager());
    }

    IEnumerator ActionManager() {
        int time = (GameManager.Instance.gameTimeHour * 60) + GameManager.Instance.gameTimeMinute;
        int startTime;

        for (i = 0; i < actions.Length; i++) {

            // Makes a single number for the start time
            startTime = (actions[i].timeslot.startTime.hour * 60) + actions[i].timeslot.startTime.minute;
            Debug.Log("Time" + time + " & " + startTime);
            // if the current time is greater than the time the action was suppose to start
            if (time > startTime) {
                Debug.Log("Welp");
                // Then if the current time is still within the duration that the action was suppose to start
                if (time < startTime + actions[i].timeslot.duration ) {
                    // Set there current position and the new duration (how long it should take them to finish the action now)

                    // if (actions[i].type == "WalkX") {
                    //     startPos = actions[i].startPosition.x;
                    //     endPos = actions[i].endPosition.x;
                    //     duration = (startTime + actions[i].timeslot.duration) - time;
                    //     float step = (Mathf.Abs(endPos - startPos) / actions[i].timeslot.duration);
                    //     transform.position = new Vector2((startPos + step * (time - startTime)), transform.position.y);
                    //     Debug.Log((startPos + step * (time - startTime)));
                    // } else if (actions[i].type == "WalkY") {
                    //     startPos = actions[i].startPosition.y;
                    //     endPos = actions[i].endPosition.y;
                    //     duration = (startTime + actions[i].timeslot.duration) - time;
                    //     float step = (Mathf.Abs(endPos - startPos) / actions[i].timeslot.duration);
                    //     transform.position = new Vector2(transform.position.x, (startPos + step * (time - startTime)));
                    // }

                    if (actions[i].type == "WalkX") {
                        // startPos = actions[i].startPosition.x;
                        // endPos = actions[i].endPosition.x;
                        Debug.Log("Walking!");
                        duration = (startTime + actions[i].timeslot.duration) - time;
                        float step = (Mathf.Abs(actions[i].endPosition.x - actions[i].startPosition.x) / actions[i].timeslot.duration);
                        transform.position = new Vector3((actions[i].startPosition.x - step * (time - startTime)), transform.position.y, transform.position.z);
                        Debug.Log(actions[i].startPosition.x);
                        Debug.Log((actions[i].startPosition.x - step * (time - startTime)));
                    } else if (actions[i].type == "WalkY") {
                        // startPos = actions[i].startPosition.y;
                        // endPos = actions[i].endPosition.y;
                        duration = (startTime + actions[i].timeslot.duration) - time;
                        float step = (Mathf.Abs(actions[i].endPosition.y - actions[i].startPosition.y) / actions[i].timeslot.duration);
                        transform.position = new Vector3(transform.position.x, (actions[i].startPosition.y + step * (time - startTime)), transform.position.z);
                    }
                }
                else {
                    Debug.Log("Continue");
                    continue;
                }
            }
            // If you /are/ on time then set the duration as normal
            else if  (time == startTime) {
                duration = actions[i].timeslot.duration;
                transform.position = new Vector2(actions[i].startPosition.x, actions[i].startPosition.y);
                Debug.Log("Time == StartPosition: " + actions[i].startPosition);
            }
            
           
            conversation = actions[i].startDialogue;

            string type = actions[i].type;
            if (type == "Idle") {
                yield return StartCoroutine(Idle());
            } else if (type == "WalkX" || type == "WalkY") {
                yield return StartCoroutine(Walk());
            } else if (type == "SwitchScene") {
                //SceneManager.LoadScene(actions[i].scene);
                Debug.Log("Switch!");
            }
        }
    }

    //Need to make function to determine walk speed based on duration
    IEnumerator Walk() {
        walking = true;
        if (actions[i].type == "WalkX") {
            startPos = actions[i].startPosition.x;
            endPos = actions[i].endPosition.x;
        } else if (actions[i].type == "WalkY") {
            startPos = actions[i].startPosition.y;
            endPos = actions[i].endPosition.y;
        }
        Debug.Log("StartPos: " + startPos + " EndPos: " + endPos);
        yield return new WaitForSecondsRealtime(duration);
        walking = false;
    }

    IEnumerator Idle() {
       transform.position = new Vector2(actions[i].startPosition.x, actions[i].startPosition.y);
       yield return new WaitForSecondsRealtime(duration);
    }
}
