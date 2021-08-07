using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Schedule schedule;
    public string name;
    private ScheduleManager scheduleManager;

    void Start() {
        scheduleManager = GetComponent<ScheduleManager>();
        if (GameManager.Instance.schedules.ContainsKey(name)) {
            schedule = GameManager.Instance.schedules[name];
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