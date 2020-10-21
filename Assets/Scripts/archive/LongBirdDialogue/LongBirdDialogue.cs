using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LongBirdDialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public TextMeshProUGUI choice1;
    public string text1;
    public TextMeshProUGUI choice2;
    public string text2;
    public TextMeshProUGUI choice3;
    public string text3;
    public TextMeshProUGUI choice4;
    public string text4;
    public GameObject choices;

    public Stereotype stereotype;

    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        textDisplay.text = "";
        index = 0;
        StartCoroutine(Type());
        choice1.text = text1;
        choice2.text = text2;
        choice3.text = text3;
        choice4.text = text4;
    }

    void Update() {
        // Debug.Log(textDisplay);
        // Debug.Log(sentences);
        // Debug.Log(index);
        // Debug.Log(continueButton);
        //if (textDisplay != null && continueButton != null) {
            if(textDisplay.text == sentences[index]) {
                continueButton.SetActive(true);
            }
        //}
    }

    IEnumerator Type() {
        foreach(char letter in sentences[index].ToCharArray()) {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence() {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1) {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        } else {
            textDisplay.text = "";
            choices.SetActive(true);
        }
    }


}
