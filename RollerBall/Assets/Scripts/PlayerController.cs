using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private const float JUMP = 250;
	private int count;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		DisplayScore ();
		winText.text = "";
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

		MovementController ();

	}

	void MovementController ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rb.AddForce (movement * speed);
	}

	void InputController ()
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
