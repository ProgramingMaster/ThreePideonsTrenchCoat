using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class Clock : MonoBehaviour {
    private Text textClock;
    int hour;
    int minute;
    float second;

    void Awake (){
        textClock = GetComponent<Text>();
        second = 0;
        minute = 0;
        hour = 2;
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