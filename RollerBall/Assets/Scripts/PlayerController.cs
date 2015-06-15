using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed;

	private Rigidbody rb;
	private const float JUMP = 250;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}

	/*
	 * Called before rendering a frame.
	 * Most of the game code goes here.
	 */
	void Update ()
	{
		HandleKeyPresses ();
	}

	/*
	 * Called before performing any physics calculations.
	 * All Physics Code goes here.
	 */
	void FixedUpdate ()
	{

		HandleMovement ();

	}

	void HandleMovement ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rb.AddForce (movement * speed);
	}

	void HandleKeyPresses ()
	{
		if (Input.GetKeyDown ("space")) {
			rb.AddForce (Vector3.up * JUMP);
		}

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
		}
	}
}
