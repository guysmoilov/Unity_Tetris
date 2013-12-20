using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float speed = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		transform.Rotate(new Vector3(0, 0, 90) * speed * Time.fixedDeltaTime);
	}
}
