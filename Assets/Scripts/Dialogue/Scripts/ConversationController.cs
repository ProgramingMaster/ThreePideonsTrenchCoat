using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class QuestionEvent : UnityEvent<Question> {}

public class ConversationController : MonoBehaviour
{
    private Conversation conversation;
    public QuestionEvent questionEvent;
    public QuestionController questionController;
    public TextMeshProUGUI textDisplay;
    public float typingSpeed;
    public GameObject continueButton;

    private int activeLineIndex = 0;
    private bool conversationStarted = false;

    public void ChangeConversation(Conversation nextConversation) {
        conversationStarted = false;
        conversation = nextConversation;
        AdvanceLine();
    }

    public void StartNewConversation(Conversation NewConversation) {
        conversation = NewConversation;
        AdvanceLine();
    }

    public void AdvanceLine() {
        textDisplay.text = "";
        if (conversation == null) {
            EndConversation();
        }
        if (!conversationStarted) Initialize();

        if (activeLineIndex < conversation.lines.Length) {
            Debug.Log("Display");
            DisplayLine();
        }
        else {
            Debug.Log("Advance");
            AdvanceConversation();
        }
    }

    private void Initialize() {
        conversationStarted = true;
        activeLineIndex = 0;
        textDisplay.text = "";
    }

    private void EndConversation() {
        conversation = null;
        conversationStarted = false;
        continueButton.SetActive(false);
        textDisplay.text = "";
    }

    private void DisplayLine() {
        Line line = conversation.lines[activeLineIndex];

        StartCoroutine(Type(line));
        continueButton.SetActive(false);
        activeLineIndex ++;        
    }
    
    void Start()
    {
        textDisplay.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (conversation != null && conversationStarted) {
            if(textDisplay.text == conversation.lines[activeLineIndex-1].text) {
                continueButton.SetActive(true);
            }
        }
    }

    IEnumerator Type(Line line) {
        foreach(char letter in line.text.ToCharArray()) {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void AdvanceConversation() {
        if (conversation.question != null)
            questionEvent.Invoke(conversation.question);
        else if (conversation.nextConversation != null)
            ChangeConversation(conversation.nextConversation);
        else
            EndConversation();
    }

    // public void _AdvanceConversation() {
    //     continueButton.SetActive(false);
        
    //     if (activeLineIndex < conversation.lines.Length - 1) {
    //         activeLineIndex++;
    //         textDisplay.text = "";
    //         StartCoroutine(Type());
    //     } else {
    //         textDisplay.text = "";
    //         activeLineIndex = 0;
    //     }
    // }
}
