// can i please have a like
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {
	[Header("Events")]
	[Space]

	public UnityEvent ToTrenchcoatEvent;

	public UnityEvent ToBirdFlyEvent;

	public UnityEvent ToBirdLandEvent;

	public int walkState; //1 = Trenchcoat, 2 = Bird Flying, 3 = Bird Walking
	public bool FacingRight = true;

	public GameObject trenchcoat;
	public GameObject birdFly;
	public GameObject birdWalk;
    
    public TrechcoatController controller;
	public ControlledFlight flyController;
	public BirdWalk birdWalkController;

	//Events

	//public EnableLedges ledge;
	Rigidbody2D rb;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
	float verticalMove = 0f;
    bool jump = false;
    //bool crouch = false;
	bool active;
		
	void Start () {
		if (ToTrenchcoatEvent == null)
			ToTrenchcoatEvent = new UnityEvent();
		if (ToBirdFlyEvent == null) 
			ToBirdFlyEvent = new UnityEvent();
		if (ToBirdLandEvent == null)
			ToBirdLandEvent = new UnityEvent();

    	rb = GetComponent<Rigidbody2D>();
		if (walkState == 2)
			active = false;
		else
			active = true;

		if (walkState == 1)
			ToTrenchcoat();
		else if (walkState == 2)
			ToBirdFly();
		else
			ToBirdWalk();
	}

    // Update is called once per frame
    void Update () {
		horizontalMove =  Input.GetAxisRaw("Horizontal") * runSpeed;
		verticalMove =  Input.GetAxisRaw("Vertical") * runSpeed;
		if ((horizontalMove < 0 && FacingRight) || (horizontalMove > 0 && !FacingRight)) {
            Flip();
        }

		if (Input.GetButtonDown("Fly")){
			if (walkState == 2 || walkState == 3)
				ToTrenchcoat();
			else if (walkState == 1) 
				ToBirdFly();

			if (walkState == 2)
				active = false;
			else
				active = true;
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

		if (walkState == 3) {
			if (Input.GetButtonDown("Jump")) {
				ToBirdFly();
			}
		}

		if (walkState == 1) {
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
    }

	private void ToBirdWalk() {
		ToBirdLandEvent.Invoke();
		trenchcoat.SetActive(false);
		birdFly.SetActive(false);
		birdWalk.SetActive(true);
		walkState = 3;
		rb.gravityScale = 5;
		GameManager.Instance.inTrenchcoat = false;
	}

	private void ToBirdFly() {
		ToBirdFlyEvent.Invoke();
		trenchcoat.SetActive(false);
		birdFly.SetActive(true);
		birdWalk.SetActive(false);
		walkState = 2;
		rb.gravityScale = 0;
		GameManager.Instance.inTrenchcoat = false;
	}

	private void ToTrenchcoat() {
		ToTrenchcoatEvent.Invoke();
		birdFly.SetActive(false);
		trenchcoat.SetActive(true);
		birdWalk.SetActive(false);
		walkState = 1;
		rb.gravityScale = 1;
		GameManager.Instance.inTrenchcoat = true;
		Debug.Log(GameManager.Instance.inTrenchcoat);
	}

	public void OnGround() {
		if (walkState == 2) {
			ToBirdWalk();
		}
	}
  
    void FixedUpdate () {
        // Move our character
		if (walkState == 3) {
			birdWalkController.Move(horizontalMove * Time.fixedDeltaTime);
		}
		if (walkState == 1) {
        	controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		}
        jump = false; //What does this do?
		if (walkState == 2) {
			flyController.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);
		}
    }

	void Flip() {
        FacingRight = !FacingRight;

        transform.Rotate (0f, 180f, 0);
    }
}