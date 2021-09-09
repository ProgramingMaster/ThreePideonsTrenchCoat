using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class enableWallLine : MonoBehaviour
{
    [Header("Events")]
	[Space]

	public UnityEvent BelowGroundColliderHit;

    public BoxCollider2D wallLineCollider;
    //public GameObject player;
    //private Rigidbody2D playerRigidbody2D;

    void Start() {
        //playerRigidbody2D = player.GetComponent<Rigidbody2D>();
        if (BelowGroundColliderHit == null)
			BelowGroundColliderHit = new UnityEvent();
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Player") {
            Debug.Log("belowGroundColliderHit");
            enableWallLines();
            //wallLineCollider.enabled = true;
            BelowGroundColliderHit.Invoke();
            // playerRigidbody2D.velocity = new Vector2(0, 0);
            // playerRigidbody2D.gravityScale = 0;
        }
    }

    private void enableWallLines() {
        //Debug.Log("Enable Wall Line Fucntion Called");
        GameObject[] GOs = GameObject.FindGameObjectsWithTag("WallLine");
		// now all your game objects are in GOs,
		// all that remains is to getComponent of each and every script and you are good to go.
		// to disable a components
		for (int i=0; i<GOs.Length; i++) {
            // Debug.Log("WallLine Enabled");
			// to access component - GOs[i].GetComponent.<BoxCollider>()
			// but I do it everything in 1 line.
			GOs[i].GetComponent<BoxCollider2D>().enabled = true;
		}
    }
}
