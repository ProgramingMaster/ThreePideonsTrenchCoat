using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct TimeFormat {
    public int hour;
    public int minute;
}

[System.Serializable]
public struct TimeSlot {
    public TimeFormat startTime;
    public int duration;
}

[System.Serializable]
public struct Action {
    public TimeSlot timeslot;
    public string type;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public Animation anim;
    public Conversation startDialogue;
    public string scene;
}

[CreateAssetMenu(fileName = "New Schedule", menuName = "Schedule")]
public class Schedule: ScriptableObject {
   public Action[] schedule;
}
