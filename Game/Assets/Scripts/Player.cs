using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed = 1.0f;
	public float jumpSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		Vector2 vel = GetComponent<Rigidbody2D> ().velocity;

		float ha = Input.GetAxisRaw ("Horizontal") * moveSpeed;
		float va = Input.GetAxisRaw ("Vertical") * jumpSpeed;

		if (GetComponent<Rigidbody2D> ().velocity.y != 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, vel.y);
		} else {
			GetComponent<Rigidbody2D> ().velocity = new Vector2(ha, va);
		}
	}
}
