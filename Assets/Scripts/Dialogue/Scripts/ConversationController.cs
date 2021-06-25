using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
    public TMP_Text textDisplay;
    public float typingSpeed;
    public GameObject continueButton;

    private int activeLineIndex = 0;
    private bool conversationStarted = false;
    private Image dialoguebox;

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
        dialoguebox.gameObject.SetActive(true);
        if (conversation == null) {
            EndConversation();
        }
        if (!conversationStarted) Initialize();

        // Debug.Log(conversation);
        // Debug.Log(activeLineIndex);
        if (activeLineIndex < conversation.lines.Length) {
            DisplayLine();
        }
        else {
            AdvanceConversation();
        }
    }

    private void Initialize() {
        conversationStarted = true;
        activeLineIndex = 0;
        textDisplay.text = "";

        dialoguebox.gameObject.SetActive(true);
    }

    private void EndConversation() {
        conversation = null;
        conversationStarted = false;
        continueButton.SetActive(false);
        textDisplay.text = "";
        dialoguebox.gameObject.SetActive(false);
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
        dialoguebox = gameObject.GetComponent(typeof(Image)) as Image;
        dialoguebox.gameObject.SetActive(false);
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
