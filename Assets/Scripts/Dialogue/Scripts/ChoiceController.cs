using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class ConversationChangeEvent : UnityEvent<Conversation> {}

public class ChoiceController : MonoBehaviour
{
    public Choice choice;
    public ConversationChangeEvent conversationChangeEvent;

    public static ChoiceController AddChoiceButton(Text choiceButtonTemplate, Choice choice, int index) {
        Debug.Log(choiceButtonTemplate);
        int buttonSpacing = -44;
        Text button = Instantiate(choiceButtonTemplate);
        Debug.Log(button);
        Debug.Log("Choice = " + choice);

        button.transform.SetParent(choiceButtonTemplate.transform.parent);
        button.transform.localScale = Vector3.one;
        button.transform.localPosition = new Vector3(0, index * buttonSpacing, 0); 
        button.name = "Choice " + (index + 1);
        button.gameObject.SetActive(true);

        ChoiceController choiceController = button.GetComponent<ChoiceController>();
        choiceController.choice = choice;
        return choiceController;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (conversationChangeEvent == null)
            conversationChangeEvent = new ConversationChangeEvent();

        GetComponent<Text>().text = choice.text;
    }

    public void MakeChoice() {
        Debug.Log("Make Choice: " + choice.conversation);
        conversationChangeEvent.Invoke(choice.conversation);
        if (choice.subSchedule != null) {
            GameObject character = GameObject.Find("Characters/" + choice.characterToChange);
            //Debug.Log(character);
            CharacterManager characterManager = character.GetComponent(typeof(CharacterManager)) as CharacterManager;
            characterManager.ChangeSchedule(choice.subSchedule);
        }
        if (choice.effectCondition != null) {
            GameManager.Instance.conditions[choice.effectCondition] = true;
        }
    }
}
