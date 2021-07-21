using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class CharacterManager : MonoBehaviour
// {
//     public string name;
//     private Schedule schedule;
//     private ScheduleManager scheduleManager;

//     void Start() {
//         scheduleManager = GetComponent<ScheduleManager>();
//         schedule = GameManager.Instance.schedules[name];
//         scheduleManager.StartAction();
//     }

//     public void ChangeSchedule(Schedule _schedule) {
//         schedule = _schedule;
//         scheduleManager.StartAction();
//     }
// }

public class CharacterManager : MonoBehaviour
{
    public Schedule schedule;
    public string name;
    private ScheduleManager scheduleManager;

    void Start() {
        scheduleManager = GetComponent<ScheduleManager>();
        if (GameManager.Instance.schedules.ContainsKey(name)) {
            schedule = GameManager.Instance.schedules[name];
            Debug.Log("Hey, Sup: " + GameManager.Instance.schedules["Wazu"].schedule[0].timeslot.duration);
            Debug.Log("Hey, Sup (2): " + GameManager.Instance.schedules["Wazu"].schedule[0].startDialogue);
        } else {
            GameManager.Instance.schedules[name] = schedule;            
        }
        Debug.Log("CharacterManager!: " + GameManager.Instance.schedules[name]);
        scheduleManager.StartAction();
    }

    public void ChangeSchedule(Schedule _schedule) {
        schedule = _schedule;
        GameManager.Instance.schedules[name] = schedule;
        scheduleManager.StartAction();
    }
}