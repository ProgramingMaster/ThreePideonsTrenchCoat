using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {
    // public string name;
    // public float[] position;

    // public SaveData(Player player) {
    //     name = player.name;
    //     position = new float[2];
    //     position[0] = player.transform.position.x;
    //     position[1] = player.transform.position.y;
    // }
    public bool MetLongBird;
    public bool MetToggy;
    public bool WazuFollower;
    public bool FluffFollower;
    public bool HasTrenchcoat;
    public bool LookingForTrenchcoat;

    public SaveData(Dictionary<string, bool> data) {
        MetLongBird = data["MetLongBird"];
        MetToggy = data["MetToggy"];
        
    }
}
