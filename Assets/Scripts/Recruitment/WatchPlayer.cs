using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchPlayer : MonoBehaviour
{
    public GameObject player;
    SpriteRenderer sprite;
    bool facingLeft;

    // Start is called before the first frame update
    void Start()
    {
        facingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < player.transform.position.x && facingLeft) {
            facingLeft = false;
            transform.Rotate (0f, 180f, 0);
        }
        if (transform.position.x > player.transform.position.x && !facingLeft) {
            facingLeft = true;
            transform.Rotate (0f, 180f, 0);
        }
    }
}
