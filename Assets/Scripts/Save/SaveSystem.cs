﻿using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    
    public static void Save (Dictionary<string, bool> gameData) {
        Debug.Log("Save");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "player.trash");
        FileStream stream = new FileStream(path, FileMode.Create);

        //SaveData data = new SaveData(gameData);

        formatter.Serialize(stream, gameData);
        stream.Close();
    }
    public static Dictionary<string, bool> LoadData() {
        Debug.Log("Load");
        string path = Path.Combine(Application.persistentDataPath, "player.trash");
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            //SaveData data = formatter.Deserialize(stream) as SaveData;
            Dictionary<string, bool> data = formatter.Deserialize(stream) as Dictionary<string, bool>;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
