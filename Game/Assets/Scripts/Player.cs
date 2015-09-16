using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public int jumpTimes = 2;
	public float moveSpeed = 1.0f;
	public float jumpSpeed = 1.0f;

	public GameObject arrow;

	private Vector2 facing = new Vector2(1, 0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) {
			facing = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
		}

		Vector2 vel = GetComponent<Rigidbody2D> ().velocity;

		float ha = Input.GetAxisRaw ("Horizontal") * moveSpeed;

		if (Input.GetButtonDown ("Jump") && jumpTimes > 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, jumpSpeed);
			jumpTimes--;
		} else {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, vel.y);
		}

		if (Input.GetButtonDown ("Ability1")) {
			GameObject a = (GameObject) Instantiate(arrow, GetComponent<Transform>().position, Quaternion.identity);
			a.GetComponent<Rigidbody2D>().velocity = facing * 10;
		}
	}

	void OnCollisionEnter2D (Collision2D hit) {
		if (hit.gameObject.tag == "Ground") {
			jumpTimes = 2;
		}
	}
}
