using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Follow : MonoBehaviour
{
    bool isNearPlayer;
    public float speed;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isNearPlayer == false) {
            //super easy, barely an inconvience
            float adjustedSpeed = speed + Math.Abs((player.transform.position.x - transform.position.x) + (player.transform.position.y - transform.position.y)) * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, adjustedSpeed);
        }
    }


    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            isNearPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            isNearPlayer = false;
        }
    }
}
