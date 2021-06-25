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
    [@System.NonSerialized]
    public Conversation conversation;

    // Start is called before the first frame update
    void Start()
    {
        //transform = GetComponent<Transform>();
        walking = false;
        i = 0;
        StartAction();
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
        }
    }

    public void StartAction() {
        schedule = GetComponent<CharacterManager>().schedule;
        actions = schedule.schedule;
        StartCoroutine(ActionManager());
    }

    IEnumerator ActionManager() {
       for (i = 0; i < actions.Length; i++) {
           conversation = actions[i].startDialogue;
           string type = actions[i].type;
           if (type == "Idle") {
               yield return StartCoroutine(Idle());
           } else if (type == "WalkX" || type == "WalkY") {
               yield return StartCoroutine(Walk());
           }

       }
    }

    //Need to make function to determine walk speed based on duration
    IEnumerator Walk() {
        walking = true;
        if (actions[i].type == "WalkX") {
            startPos = actions[i].startPosition.x;
            endPos = actions[i].endPosition.x;
        } else {
            startPos = actions[i].startPosition.y;
            endPos = actions[i].endPosition.y;
        }
        yield return new WaitForSecondsRealtime(actions[i].timeslot.duration);
        walking = false;
    }

    IEnumerator Idle() {
       transform.position = new Vector2(actions[i].startPosition.x, actions[i].startPosition.y);
       yield return new WaitForSecondsRealtime(actions[i].timeslot.duration);
    }
}
