﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using ConversationController;
using TMPro;

public class TalkButton : MonoBehaviour
{
    public GameObject TalkButtonPrefab;
    public float talkButtonOffset;
    public ConversationController conversationController;
    public string followConditionName;
    private Conversation NewConversation;

    private GameObject talkButton;
    private Button button;
    private Animator anim;
    
    bool created = false;
    bool started = false;
    bool follower;
    // Start is called before the first frame update
    void Start()
    {
        //anim = button.GetComponent<Animator>();
    }

    private bool checkIfFollower() {
        if (followConditionName != "") {
            if (GameManager.Instance.conditions[followConditionName] == true) {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        follower = checkIfFollower();
        if (collider.tag == "Player" && !follower) {
            // Create talk popup if not already created
            NewConversation = GetComponent<ScheduleManager>().conversation;
            Debug.Log("Talk: " + NewConversation);
            if (!created) {
                //Vector2 position = new Vector2(transform.position.x, transform.position.y+talkButtonOffset);
                //talkButton = Instantiate(TalkButtonPrefab, position, Quaternion.identity, transform);
                //talkButton = GameObject.GetComponentInChildren(typeof(GameObject)) as GameObject;
                talkButton = gameObject.transform.GetChild(0).gameObject;
                anim = talkButton.GetComponentInChildren(typeof(Animator)) as Animator;
                button = talkButton.GetComponentInChildren(typeof(Button)) as Button;
                created = true;

                button.onClick.AddListener(ButtonClicked);
            } else { // show it
                started = true;
                talkButton.SetActive(true);
                //StartCoroutine("animStarted");
            }
            //StartCoroutine("NewPopup");
        }
    }

    private void ButtonClicked() {
        talkButton.SetActive(false);
        Debug.Log("Converstion (Clicked) = " + NewConversation);
        conversationController.StartNewConversation(NewConversation);
    }

    IEnumerator animStarted() {
        yield return new WaitForSeconds(0.3f);
        started = false;
    }

    // IEnumerator popup() {
    //     anim.Play("Newpopup");
    //     yield return new WaitForSeconds(0.2f);
    // }

    private void OnTriggerExit2D (Collider2D collider) {
        if (!follower)
            StartCoroutine("popdown");
    }

    IEnumerator popdown() {
        //yield return new WaitForSeconds(0.5f);
        anim.Play("NewPopdown");
        yield return new WaitForSeconds(0.2f);
        talkButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
