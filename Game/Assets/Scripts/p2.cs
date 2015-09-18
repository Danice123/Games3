using UnityEngine;
using System.Collections;

public class p2 : MonoBehaviour {

	public int jumpTimes = 3;
	public float moveSpeed = 1.0f;
	public float jumpSpeed = 1.0f;
	
	public GameObject sword;
	public int jabspeed = 2;
	public GameObject IsSword;
	public GameObject shield;
	public float shieldtime = 3f;
	public GameObject IsShield;
	
	public Vector2 facing = new Vector2(1, 0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void FixedUpdate() {
		shieldtime -= Time.deltaTime;
		if (GetComponent<Health> ().health <= 0) {
			DestroyObject (gameObject);
		}
		
		if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) {
			facing = new Vector2 (Input.GetAxisRaw ("Horizontal"), -Input.GetAxisRaw ("Vertical")).normalized;
		}
		
		Vector2 vel = GetComponent<Rigidbody2D> ().velocity;
		
		if (!Input.GetButton ("Y")) {
			float ha = Input.GetAxisRaw ("Horizontal") * moveSpeed;
			
			if (Input.GetButtonDown ("A") && jumpTimes > 0) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, jumpSpeed);
				jumpTimes--;
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, vel.y);
			}
		}
		
		if (Input.GetButtonDown ("Y") && shieldtime <=0) {
			shieldtime = 3.0f;
			float angle = Mathf.Atan2 (facing.y, facing.x) * Mathf.Rad2Deg;
			
			IsShield = (GameObject)Instantiate (shield, GetComponent<Transform> ().position, Quaternion.AngleAxis (angle, Vector3.forward));

		}
		
		if (Input.GetButtonDown ("X")) {

			if (IsSword == null){
			 IsSword = (GameObject)Instantiate (sword, GetComponent<Transform> ().position, Quaternion.identity);
				IsSword.GetComponent<Rigidbody2D>().velocity = new Vector2(facing.x * jabspeed +GetComponent<Rigidbody2D> ().velocity.x , 0); 
				IsSword.GetComponent<sword>().owner = gameObject.tag;


			}
		}


	}





	void OnCollisionEnter2D (Collision2D hit) {
		if (hit.gameObject.tag == "Ground") {
			jumpTimes = 3;
		}
	}


}
