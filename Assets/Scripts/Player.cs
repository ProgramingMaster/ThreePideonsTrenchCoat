using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string name = "Caspian";

    public void SavePlayer ()
    {
        SaveSystem.Save(this);
    }

    public void LoadPlayer () {
        SaveData data = SaveSystem.LoadData();

        name = data.name;

        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];
        transform.position = position;
    }
}
