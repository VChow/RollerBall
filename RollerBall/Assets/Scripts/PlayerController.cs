using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerController : MonoBehaviour
{
	public float speed;
	public float accelerometerSpeed;
	public Text countText;
	public Text winText;
	public Text debugLogger;

	private Rigidbody rb;
	private const float JUMP = 250;
	private int count;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		DisplayScore ();
		winText.text = "";

		Screen.orientation = ScreenOrientation.Portrait;
	}
	
	/*
	 * Called before rendering a frame.
	 * Most of the game code goes here.
	 */
	void Update ()
	{
		InputController ();
	}

	/*
	 * Called before performing any physics calculations.
	 * All Physics Code goes here.
	 */
	void FixedUpdate ()
	{
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			WindowsMovementController ();
		} else {
			AndroidMovementController ();
	
		}
	}

	void WindowsMovementController ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rb.AddForce (movement * speed);
	}

	void AndroidMovementController ()
	{
		//Vector3 movement = Vector3.zero;
		//dir.x = -Input.acceleration.y;
		//dir.z = Input.acceleration.x;

		//movement.x = Input.acceleration.x; //Left/Right working!
		//movement.z = -Input.acceleration.y; //extreme tilt down, causes bouncing.

		//movement.y = -Input.acceleration.y; //bounces (tilting down towards causes jumping, tilting away stops bouncing.
		//movement.y = Input.acceleration.z; //No movement. can jump if jumping the phone.
		//movement.y = Input.acceleration.y; doesnt move. occasionally small bounces.
		//movement.y = -Input.acceleration.z; Bounces stationary

		//movement.z = Input.acceleration.z; Bounces around, moves up down
		//movement.z = -Input.acceleration.z; Bounces around, moves up down
		//movement.z = -Input.acceleration.y; Does nothing
		//movement.z = Input.acceleration.y; Randomly bouncing, does move

		//if (movement.sqrMagnitude > 1)
		//movement.Normalize ();

		//movement *= Time.deltaTime;
		//transform.Translate (movement * speed);

		//===
		float moveHorizontal = Input.acceleration.x;
		float moveVertical = Input.acceleration.y;
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rb.AddForce (movement * accelerometerSpeed);

		//===

		debugLogger.text = "X: " + movement.x + " Y: " + movement.y + " Z: " + Input.acceleration.z;
	}
	
	void InputController ()
	{
		//Space - Jump
		if (Input.GetKeyDown (KeyCode.Space)) {
			//rb.AddForce (Vector3.up * JUMP);
		}

		//R - Reset
		if (Input.GetKeyDown (KeyCode.R)) {
			rb.MovePosition (new Vector3 (0, 0, 0));
			rb.isKinematic = true;
			rb.isKinematic = false;

		}

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);

			//Incremenet score
			count ++;
			DisplayScore ();
		}
	}

	void DisplayScore ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) {
			winText.text = "You Win!";
		}
	}
}
