using UnityEngine;
using System.Collections;

public class p2 : MonoBehaviour {


	
	public GameObject sword;
	public int jabspeed = 2;
	public GameObject IsSword;
	public GameObject shield;
	public float shieldtime = 3f;
	public GameObject IsShield;
	


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void FixedUpdate() {
		Vector2 facing = GetComponent<Player> ().facing;
		shieldtime -= Time.deltaTime;

		

		
		if (!Input.GetButton ("Y")) {
			float ha = Input.GetAxisRaw ("Horizontal") * GetComponent<Player> ().moveSpeed;
			

		}
		
		if (Input.GetButtonDown ("Y") && shieldtime <=0) {
			Debug.Log ("test");
			shieldtime = 3.0f;
			float angle = Mathf.Atan2 (facing.y, facing.x) * Mathf.Rad2Deg;
			
			IsShield = (GameObject)Instantiate (shield, GetComponent<Transform> ().position, Quaternion.AngleAxis (angle, Vector3.forward));
			IsShield.GetComponent<shield> ().player = gameObject;

		}
		
		if (Input.GetButtonDown ("X")) {

			if (IsSword == null){
			 IsSword = (GameObject)Instantiate (sword, GetComponent<Transform> ().position, Quaternion.identity);
				IsSword.GetComponent<Rigidbody2D>().velocity = new Vector2(facing.x * jabspeed +GetComponent<Rigidbody2D> ().velocity.x , 0); 
				IsSword.GetComponent<sword>().owner = gameObject.tag;
				IsSword.GetComponent<sword> ().player = gameObject;

			}
		}


	}







}
