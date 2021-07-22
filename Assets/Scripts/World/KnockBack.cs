using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public GameObject player;
    public float knockBackDistance;
    public string disableCondition;
    public bool conditionState;
    public bool NTBIT;
    bool disabilty;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        if (disableCondition != null)
            disabilty = true;
        else
            disabilty = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (disabilty == true) {
            if (GameManager.Instance.conditions[disableCondition] == conditionState) {
                return;
            }
        }
        if (NTBIT == true) {
            if (GameManager.Instance.inTrenchcoat == true) {
                return;
            }
        }
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
