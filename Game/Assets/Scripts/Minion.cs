using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {

	public bool isLeftMinion = true;
	public float moveSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		Vector2 vel = GetComponent<Rigidbody2D> ().velocity;

		if (isLeftMinion)
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, vel.y);
		else
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, vel.y);
	}
}
