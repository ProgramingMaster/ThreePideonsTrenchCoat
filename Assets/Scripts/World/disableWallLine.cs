using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class disableWallLine : MonoBehaviour
{

    [Header("Events")]
    [Space]

	public UnityEvent AboveGroundColliderHit;

    public BoxCollider2D wallLineCollider;

    // Start is called before the first frame update
    void Start()
    {
        if (AboveGroundColliderHit == null)
			AboveGroundColliderHit = new UnityEvent();
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Player") {
            Debug.Log("aboveGroundcolliderHit");
            disableWallLines();
            //wallLineCollider.enabled = false;
            AboveGroundColliderHit.Invoke();
        }
    }

    private void disableWallLines() {
        //ßDebug.Log("")
        GameObject[] GOs = GameObject.FindGameObjectsWithTag("WallLine");
		// now all your game objects are in GOs,
		// all that remains is to getComponent of each and every script and you are good to go.
		// to disable a components
		for (int i=0; i<GOs.Length; i++) {
			// to access component - GOs[i].GetComponent.<BoxCollider>()
			// but I do it everything in 1 line.
			GOs[i].GetComponent<BoxCollider2D>().enabled = false;
		}
    }
}
