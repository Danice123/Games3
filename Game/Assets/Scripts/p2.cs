using UnityEngine;
using System.Collections;

public class p2 : MonoBehaviour {

	public int jumpTimes = 3;
	public float moveSpeed = 1.0f;
	public float jumpSpeed = 1.0f;
	
	public GameObject sword;
	public int swingSpeed = 10;
	
	public GameObject shield;
	public float shieldtime = 0.5f;
	
	private Vector2 facing = new Vector2(1, 0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void FixedUpdate() {
		if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) {
			facing = new Vector2(Input.GetAxisRaw("Horizontal"), -Input.GetAxisRaw("Vertical")).normalized;
		}
		
		Vector2 vel = GetComponent<Rigidbody2D> ().velocity;
		
		float ha = Input.GetAxisRaw ("Horizontal") * moveSpeed;
		
		if (Input.GetButtonDown ("A") && jumpTimes > 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, jumpSpeed);
			jumpTimes--;
		} else {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, vel.y);
		}
		
		if (Input.GetButtonDown ("X")) {
			GameObject a = (GameObject) Instantiate(sword, GetComponent<Transform>().position, Quaternion.identity);

		}
		
		if (Input.GetButtonDown ("Y")) {
			GameObject a = (GameObject) Instantiate(shield, GetComponent<Transform>().position, Quaternion.identity);

		}
	}







	void OnCollisionEnter2D (Collision2D hit) {
		if (hit.gameObject.tag == "Ground") {
			jumpTimes = 3;
		}
	}


}
