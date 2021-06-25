using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Schedule schedule;
    private ScheduleManager scheduleManager;

    void Start() {
        scheduleManager = GetComponent<ScheduleManager>();
    }

    public void ChangeSchedule(Schedule _schedule) {
        schedule = _schedule;
        scheduleManager.StartAction();
    }
}
