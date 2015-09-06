using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public int jumpTimes = 2;
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

		if (Input.GetButtonDown ("Jump") && jumpTimes > 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, jumpSpeed);
			jumpTimes--;
		} else {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, vel.y);
		}
	}

	void OnCollisionEnter2D (Collision2D hit) {
		if (hit.gameObject.tag == "Ground") {
			jumpTimes = 2;
		}
	}
}
