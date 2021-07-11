using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public float playerHealth=100;
    public int gameTimeHour = 2;
    public int gameTimeMinute = 0;
    public Dictionary<string, bool> conditions = new Dictionary<string, bool> {
        {"AskedToggyForPassword", false},
        {"AgreedToGetPasswordForFlower", false},
        {"WazuAndFluffAreFollowers", false}
    };

    private void Awake()
    {
        Instance = this;
    }
}
