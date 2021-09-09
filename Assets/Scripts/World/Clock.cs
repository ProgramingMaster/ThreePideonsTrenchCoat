using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;

public class Clock : MonoBehaviour {
    private TMP_Text textClock;
    int hour;
    int minute;
    float second;

    void Awake (){
        textClock = GetComponent<TMP_Text>();
        second = 0;
        minute = 0;
        hour = 2;
    }

    void Start() {
        
    }

    public void TimeLoaded() {
        minute = GameManager.Instance.gameTimeMinute;
        hour = GameManager.Instance.gameTimeHour;
    }

    void Update (){
        second += Time.deltaTime;

        if (second >= 1) {
            minute++;
            second = 0;
            if (minute >= 60) {
                hour++;
                minute = 0;
            }
        }

        textClock.text = LeadingZero(hour) + ":" + LeadingZero(minute);
        GameManager.Instance.gameTimeHour = hour;
        GameManager.Instance.gameTimeMinute = minute;
    }
    string LeadingZero (int n){
        return n.ToString().PadLeft(2, '0');
    }
}