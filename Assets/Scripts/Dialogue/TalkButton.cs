using System.Collections;
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
    public Conversation NewConversation;

    private GameObject talkButton;
    private Button button;
    private Animator anim;
    
    bool created = false;
    bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        //anim = button.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        // Create talk popup if not already created
        if (!created) {
            Vector2 position = new Vector2(transform.position.x, transform.position.y+talkButtonOffset);
            talkButton = Instantiate(TalkButtonPrefab, position, Quaternion.identity, transform);
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

    private void ButtonClicked() {
        talkButton.SetActive(false);
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
        //if (!started)
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
