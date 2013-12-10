using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

	public float fallSpeed = 1;
	public float snapDownSpeed = 30;
	public float moveAmount = 1;
	public float maxLeft = -6;
	public float maxRight = 6;

	private float realFallSpeed;

	public Transform[] cubes;
	public static Transform[,] grid = new Transform[10,6];

	// Use this for initialization
	void Start () 
	{
		realFallSpeed = fallSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		if (Input.GetButtonDown("Right"))
		{
			if (transform.position.x + Vector3.right.x * moveAmount < maxRight)
			{
				transform.Translate(Vector3.right * moveAmount, Space.World);
			}
		}

		if (Input.GetButtonDown("Left"))
		{
			if (transform.position.x + Vector3.left.x * moveAmount > maxLeft)
			{
				transform.Translate(Vector3.left * moveAmount, Space.World);
			}
		}

		// Snap down
		if (Input.GetButton("Jump"))
		{
			realFallSpeed = snapDownSpeed;
		}
		else 
		{
			realFallSpeed = fallSpeed;
		}

		if (Input.GetButtonDown("Fire1"))
		{
			// Rotate left
			transform.Rotate(new Vector3(0,0,90));
		}

		if (Input.GetButtonDown("Fire2"))
		{
			// Rotate right
			transform.Rotate(new Vector3(0,0,-90));
		}

		transform.Translate(Vector3.down * Time.fixedDeltaTime * realFallSpeed, Space.World);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name == "Frame Bottom" || other.name == "Cube")
		{
			// Stop moving
			this.GetComponent<BlockController>().enabled = false;

			// Correct height
			transform.position = new Vector3(transform.position.x, 
		                       				 	Mathf.Ceil(transform.position.y) - 0.5f * moveAmount, 
			                       				transform.position.z);
		}
		else if (other.name == "Right Wall")
		{
			// Move left
			transform.Translate(Vector3.left * moveAmount, Space.World);
		}
		else if (other.name == "Left Wall")
		{
			// Move right
			transform.Translate(Vector3.left * moveAmount, Space.World);
		}
	}
}
