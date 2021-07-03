// can i please have a like
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public bool BirdMode = false;
	public bool FacingRight = true;
	public bool walking = false;

	public GameObject trenchcoat;
	public GameObject bird;
	public GameObject birdWalk;
    
    public TrechcoatController controller;
	public ControlledFlight flyController;
	public BirdWalk birdWalkController;
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
		active = !BirdMode;
		trenchcoat.SetActive(false);
		bird.SetActive(false);
	}

    // Update is called once per frame
    void Update () {
		horizontalMove =  Input.GetAxisRaw("Horizontal") * runSpeed;
		verticalMove =  Input.GetAxisRaw("Vertical") * runSpeed;
		if ((horizontalMove < 0 && FacingRight) || (horizontalMove > 0 && !FacingRight)) {
            Flip();
        }

		if (Input.GetButtonDown("Fly")){
			BirdMode = !BirdMode;
			active = !BirdMode;
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

		if (BirdMode == false) {
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
		if (walking) {
			trenchcoat.SetActive(false);
			bird.SetActive(false);
			birdWalk.SetActive(true);
			BirdMode = false;
			rb.gravityScale = 1;
		}
		else if (BirdMode) {
			trenchcoat.SetActive(false);
			bird.SetActive(true);
			birdWalk.SetActive(false);
			rb.gravityScale = 0;
		} else {
			bird.SetActive(false);
			trenchcoat.SetActive(true);
			birdWalk.SetActive(false);
			rb.gravityScale = 1;
		}

    }

	public void OnGround() {
		if (BirdMode) {
			walking = true;
		}
	}
        
    void FixedUpdate () {
        // Move our character
		if (walking) {
			birdWalkController.Move(horizontalMove * Time.fixedDeltaTime);
		}
		if (BirdMode == false && walking == false) {
        	controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		}
        jump = false;
		if (BirdMode) {
			flyController.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);
		}
    }

	void Flip() {
        FacingRight = !FacingRight;

        transform.Rotate (0f, 180f, 0);
    }
}