using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkPopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject canvas;
    public GameObject button;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = button.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        canvas.SetActive(true);
    }

    private void OnTriggerExit2D (Collider2D collider) {
        StartCoroutine("popdown");
        //Debug.Log(anim);
    }

    IEnumerator popdown() {
        anim.Play("popdown");
        yield return new WaitForSeconds(0.2f);
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
