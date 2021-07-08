using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Line
{
    [TextArea(2, 5)]
    public string text;
}
[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public Line[] lines;
    public Question question;
    public string name;
    public Conversation nextConversation;
}
