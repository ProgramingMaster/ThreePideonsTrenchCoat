using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float health = 100;

    //public GameData gamedata;
    void Start() {
        //Debug.Log(GameData.conditions);
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("You Died");
    }

    // public void SavePlayer ()
    // {
    //     Debug.Log("Save");
    //     SaveSystem.Save(this);
    // }

    // public void LoadPlayer () {
    //     Debug.Log("Load");
    //     SaveData data = SaveSystem.LoadData();

    //     name = data.name;

    //     Vector2 position;
    //     position.x = data.position[0];
    //     position.y = data.position[1];
    //     transform.position = position;
    // }

    
}
