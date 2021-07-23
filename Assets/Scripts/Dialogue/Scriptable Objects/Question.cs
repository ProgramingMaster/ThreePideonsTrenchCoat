using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[System.Serializable]
public class ScheduleEvent : UnityEvent<Schedule> {}

[System.Serializable]
public struct showCondition {
    public string condition;
    public bool state;
}

[System.Serializable]
public struct Choice {
    [TextArea(2, 5)]
    public string text;
    public Conversation conversation;
    public Schedule subSchedule;
    public string characterToChange;
    public showCondition[] showConditions;
    public string effectCondition;
    public Follower[] makeFollowers;
}

[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class Question : ScriptableObject
{
    [TextArea(2, 5)]
    public string text;
    public Choice[] choices;
}

// [CustomEditor(typeof(Choice))]
// public class MyScriptEditor : Editor
// {
//     bool changeSchedule;
//     override public void OnInspectorGUI() 
//     {

//         changeSchedule = GUILayout.Toggle(changeSchedule, "changeSchedule");
     
//         if(changeSchedule) {
//             this.serializedObject.Update();
//             EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onEvent"), true);
//             this.serializedObject.ApplyModifiedProperties();
//         }
//     }
// }