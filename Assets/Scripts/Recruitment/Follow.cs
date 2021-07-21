using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Follow : MonoBehaviour
{
    bool isNearPlayer;
    public float speed;
    public GameObject player;
    public string followConditionName;
    public SpriteRenderer sprite;

    private int state;
    // Start is called before the first frame update
    void Start()
    {
        state = -1;
    }

    // Update is called once per frame
    void Update()
    {
        // if (isNearPlayer == false) {
            //super easy, barely an inconvience
            if (state != 1) {
                float adjustedSpeed = speed + Math.Abs((player.transform.position.x - transform.position.x) + (player.transform.position.y - transform.position.y)) * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, adjustedSpeed);
            }
        //}
    }

    public void toTrenchcoat() {
        Debug.Log("Sip");
        sprite.enabled = false;
    }

    public void toBird() {
        sprite.enabled = true;
    }




    // void OnTriggerEnter2D(Collider2D other) {
    //     if(other.gameObject.CompareTag("Player")) {
    //         isNearPlayer = true;
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other) {
    //     if(other.gameObject.CompareTag("Player")) {
    //         isNearPlayer = false;
    //     }
    // }
}
