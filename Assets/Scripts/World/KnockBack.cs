using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public GameObject player;
    public float knockBackDistance;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            Vector2 newVel = new Vector2(knockBackDistance, rb.velocity.y);
            rb.velocity = newVel;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
