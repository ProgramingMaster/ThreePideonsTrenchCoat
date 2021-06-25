using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static Dictionary<string, bool> conditions = new Dictionary<string, bool>() {
        {"MetLongBird", false},
        {"MetToggy", false}
    };
    
    // void Start() {
    //     LoadGameData();
    // }

    public static void SaveGameData ()
    {
        Debug.Log("Save");
        SaveSystem.Save(conditions);
    }

    public static void LoadGameData () {
        Debug.Log("Load");
        Dictionary<string, bool> data = SaveSystem.LoadData();

        conditions = data;
        Debug.Log(conditions["MetLongBird"]);
    }
}
