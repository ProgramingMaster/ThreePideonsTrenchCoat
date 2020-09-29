// can i please have a like
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
	public bool flight = false;
	public bool FacingRight = true;
    
    public TrechcoatController controller;
	public FlightController flyController;
	//public EnableLedges ledge;
	Rigidbody2D rb;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
	float verticalMove = 0f;
    bool jump = false;
    //bool crouch = false;
	bool active;
		
	void Start () {
    	rb = GetComponent<Rigidbody2D>();
		active = !flight;
	}

    // Update is called once per frame
    void Update () {
		horizontalMove =  Input.GetAxisRaw("Horizontal") * runSpeed;
		verticalMove =  Input.GetAxisRaw("Vertical") * runSpeed;
		if ((horizontalMove < 0 && FacingRight) || (horizontalMove > 0 && !FacingRight)) {
            Flip();
        }

		if (Input.GetButtonDown("Fly")){
			flight = !flight;
			active = !flight;
			rb.rotation = 0;
			//ledge.toggle();
			GameObject[] GOs = GameObject.FindGameObjectsWithTag("ledge");
			// now all your game objects are in GOs,
			// all that remains is to getComponent of each and every script and you are good to go.
			// to disable a components
			for (int i=0; i<GOs.Length; i++) {
				// to access component - GOs[i].GetComponent.<BoxCollider>()
				// but I do it everything in 1 line.
				GOs[i].GetComponent<BoxCollider2D>().enabled = active;
			}
		}

		if (flight == false) {
			if (Input.GetButtonDown("Jump"))
			{
				jump = true;
			}
				
			// if (Input.GetButtonDown("Crouch"))
			// {
			// 	crouch = true;
			// } else if (Input.GetButtonUp("Crouch"))
			// {
			// 	crouch = false;
			// }
		}
		if (flight) {
			rb.gravityScale = 0;
		} else {
			rb.gravityScale = 1;
		}

    }
        
    void FixedUpdate () {
        // Move our character
		if (flight == false) {
        	controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		}
        jump = false;
		if (flight) {
			flyController.Fly(horizontalMove, verticalMove, FacingRight);
		}
    }

	void Flip() {
        FacingRight = !FacingRight;

        transform.Rotate (0f, 180f, 0);
    }
}