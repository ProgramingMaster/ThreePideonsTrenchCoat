using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionController : MonoBehaviour
{
    public Question question;
    public TMP_Text questionText;
    public Text choiceButton;

    private List<ChoiceController> choiceControllers = new List<ChoiceController>();

    public void Change(Question _question) {
        RemoveChoices();
        question = _question;
        gameObject.SetActive(true);
        Initialize();
    }

    // public void Hide(Conversation conversation) {
    //     RemoveChoices();
    //     gameObject.SetActive(false);
    // }

    public void Hide() {
        RemoveChoices();
        gameObject.SetActive(false);
    }

    private void RemoveChoices() {
        foreach(ChoiceController c in choiceControllers)
            Destroy(c.gameObject);

        choiceControllers.Clear();
    }

    void Start() {
        question = null;
    }

    private void Initialize() {
        questionText.text = question.text;

        // See if you should add button
        bool show;
        for (int index = 0; index < question.choices.Length; index++) {
            show = true; 
            for (int j = 0; j < question.choices[index].showConditions.Length; j++) {
                if (GameManager.Instance.conditions[question.choices[index].showConditions[j].condition] != question.choices[index].showConditions[j].state) {
                    show = false;
                    break;
                }
            }

            // if so, add button
            if (show) {
                Debug.Log("Question: " + question);
                ChoiceController c = ChoiceController.AddChoiceButton(choiceButton, question.choices[index], index);
                choiceControllers.Add(c);
            }
        }

        choiceButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
