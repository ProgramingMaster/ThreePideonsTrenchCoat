using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

[System.Serializable]
public class ConversationChangeEvent : UnityEvent<Conversation> {}

public class ChoiceController : MonoBehaviour
{
    public Choice choice;
    public ConversationChangeEvent conversationChangeEvent;
    public SummonFollowers Summon;
    public ToTrenchcoat toTrenchcoat;
    public GameObject higherSlot;
    public GameObject lowerSlot;

    public static ChoiceController AddChoiceButton(Text choiceButtonTemplate, Choice choice, int index) {
        Debug.Log(choiceButtonTemplate);
        int buttonSpacing = -22;
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
        if (choice.makeFollowers != null) {
            int i;
            GameObject follower;
            SpriteRenderer followerSprite;
            Sprite followerHead;
            Animator followerAnimator;
            for (i = 0; i < choice.makeFollowers.Length; i++) {
                if (choice.makeFollowers.Length > 2)
                    Debug.Log("Dude, that's too many followers");

                follower = GameObject.Find("Characters/" + choice.makeFollowers[i]);
                followerSprite = follower.GetComponentInChildren(typeof(SpriteRenderer)) as SpriteRenderer;
                followerAnimator = follower.GetComponent(typeof(Animator)) as Animator;
                followerHead = (follower.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite;
                
                toTrenchcoat.Setup(followerHead);
                GameManager.Instance.conditions[choice.makeFollowers[i] + "Follower"] = true;
                Summon.Summon(choice.makeFollowers[i], followerSprite.sprite, followerAnimator, higherSlot, lowerSlot, follower.transform.position);
            }
        }
    }
}
