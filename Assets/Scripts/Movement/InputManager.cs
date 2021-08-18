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

	private float wallLine;
	private float floorLine;
	public int currentlayer;

	public int walkState; //1 = Trenchcoat, 2 = Bird Flying, 3 = Bird Walking
	public bool FacingRight = true;

	public GameObject trenchcoat;
	public GameObject birdFly;
	public GameObject birdWalk;
	public CircleCollider2D playerColliderRef;
    
    public TrechcoatController controller;
	public ControlledFlight flyController;
	public BirdWalk birdWalkController;
	public layers layerScript;

	//Events

	//public EnableLedges ledge;
	Rigidbody2D rb;
	public SpriteRenderer renderer;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
	float verticalMove = 0f;
    bool jump = false;
    //bool crouch = false;
	bool active;
	float layer1;
	float layer2;
	float layer3;

	string layerName = "Layer";
		
	void Start () {
		//layer
		currentlayer = 1;
		// layer1 = (float) (wallLine - (Mathf.Abs(floorLine - wallLine) * 0.7));
		// layer2 = (float) (wallLine - (Mathf.Abs(floorLine - wallLine) * 0.4));
		// layer3 = (float) (wallLine - (Mathf.Abs(floorLine - wallLine) * 0.1));
		layer1 = (float) (layerScript.layer1 + 1.1);
		layer2 = (float) (layerScript.layer2 + 1.1);
		layer3 = (float) (layerScript.layer3 + 1.1);
		// wallLine = layerScript.wallLine;
		// floorLine = layerScript.floorLine;

		for (int j = 1; j <= 4; j++) {
			GameObject[] GOs = GameObject.FindGameObjectsWithTag(layerName + j);
			
			for (int i=0; i<GOs.Length; i++) {
				// to access component - GOs[i].GetComponent.<BoxCollider>()
				Physics2D.IgnoreCollision(playerColliderRef, GOs[i].GetComponent<BoxCollider2D>());
			}
		}
		Debug.Log(layer1 + " " + layer2 + " " + layer3);

		if (ToTrenchcoatEvent == null)
			ToTrenchcoatEvent = new UnityEvent();
		if (ToBirdFlyEvent == null) 
			ToBirdFlyEvent = new UnityEvent();
		if (ToBirdLandEvent == null)
			ToBirdLandEvent = new UnityEvent();

    	rb = GetComponent<Rigidbody2D>();
		//renderer = GetComponent<SpriteRenderer>();

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
		if (transform.position.y <= layer1) {
			currentlayer = 1;
			renderer.sortingLayerName = layerName + currentlayer;
			//layerColliderControl(currentlayer, true);
		} else {
			//layerColliderControl(currentlayer, false);
		}
		if (transform.position.y > layer1) {
			currentlayer = 2;
			renderer.sortingLayerName = layerName + currentlayer;
			layerColliderControl(currentlayer, true);
		} else {
			//layerColliderControl(currentlayer, false);
		}

		if (transform.position.y > layer2) {
			currentlayer = 3;
			renderer.sortingLayerName = layerName + currentlayer;
			//layerColliderControl(currentlayer, true);
		} else {
			//layerColliderControl(currentlayer, false);
		}
		if (transform.position.y > layer3) {
			currentlayer = 4;
			renderer.sortingLayerName = layerName + currentlayer;
			//layerColliderControl(currentlayer, true);
		} else {
			//layerColliderControl(currentlayer, false);
		}
		Debug.Log(currentlayer);

    }

	private void layerColliderControl(int layer, bool state) {
		GameObject[] GOs = GameObject.FindGameObjectsWithTag(layerName + layer);
		
		for (int i=0; i<GOs.Length; i++) {
			// to access component - GOs[i].GetComponent.<BoxCollider>()
			
			GOs[i].GetComponent<BoxCollider2D>().enabled = state;
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
		rb.gravityScale = 0;
		GameManager.Instance.inTrenchcoat = true;
		Debug.Log(GameManager.Instance.inTrenchcoat);
	}

	public void OnGround() {
		if (walkState == 2) {
			ToBirdWalk();
		}
	}
  
    void FixedUpdate () {
        // // Move our character
		// if (walkState == 3) {
		// 	birdWalkController.Move(horizontalMove * Time.fixedDeltaTime);
		// }
		// if (walkState == 1) {
        // 	controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		// }
        // jump = false; //What does this do?
		// if (walkState == 2) {
		flyController.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);
		//}
    }

	void Flip() {
        FacingRight = !FacingRight;

        transform.Rotate (0f, 180f, 0);
    }
}