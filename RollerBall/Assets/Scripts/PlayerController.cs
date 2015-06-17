using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerController : MonoBehaviour
{
#region Member Variables
	public float speed;
	public float accelerometerSpeed;

	public Text countText;
	public Text winText;
	public Text debugLogger;

	private Rigidbody rb;
	private const float JUMP = 250;
	private int count;
#endregion

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		DisplayScore ();
		winText.text = "";

		//Set screen orientation to portrait for Android devices.
		Screen.orientation = ScreenOrientation.Portrait;

	}

#region Updates
	/*
	 * Called before rendering a frame.
	 * Most of the game code goes here.
	 */
	void Update ()
	{
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			WindowsInputController ();
		} else {
			AndroidInputController ();	
		}

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
#endregion

#region Movement Controllers
	/*
	 * Handles movement input for PC.
	 */
	void WindowsMovementController ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rb.AddForce (movement * speed);
	}

	/*
	 * Handles axis/movement input for Android devices. 
	 */
	void AndroidMovementController ()
	{
		float moveHorizontal = Input.acceleration.x;
		float moveVertical = Input.acceleration.y;

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rb.AddForce (movement * accelerometerSpeed);

		//debugLogger.text = "X: " + movement.x + " Y: " + movement.y + " Z: " + Input.acceleration.z;
	}
#endregion

#region Key/Touch Input Controllers
	/*
	 * Handles key input for PC.
	 */ 
	void WindowsInputController ()
	{
		//Space - Jump
		if (Input.GetKeyDown (KeyCode.Space)) {
			rb.AddForce (Vector3.up * JUMP);
		}

		//R - Reset
		if (Input.GetKeyDown (KeyCode.R)) {
			rb.MovePosition (new Vector3 (0, 0, 0));
			rb.isKinematic = true;
			rb.isKinematic = false;
		}
	}

	/*
	 * Handles touch input for Android.
	 */ 
	void AndroidInputController ()
	{
		if (Input.GetMouseButtonDown (0)) {
			rb.AddForce (Vector3.up * JUMP);
		}
	}
#endregion



	/*
	 * Called when player hits a pick-up item.
	 */ 
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);

			//Increment score
			count ++;
			DisplayScore ();
		}
	}

	/*
	 * Display and update the score.
	 */ 
	void DisplayScore ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) {
			winText.text = "You Win!";
		}
	}
}
