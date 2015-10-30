using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class p2 : MonoBehaviour {


	
	public GameObject sword;
	public GameObject IsSword;
	public float swordtime = 2f;
	public GameObject shield;
	public float shieldtime = 3f;
	public GameObject IsShield;
	private Player player;


	


	// Use this for initialization
	void Start () {
		player = GetComponent<Player> ();
		if ((Network.isServer || Network.isClient) && !GetComponent<NetworkView> ().isMine) {
			Destroy (GetComponent<Rigidbody2D>());
		}
		Vector2 facing = GetComponent<Player> ().facing;
		float angle = Mathf.Atan2 (facing.y, facing.x) * Mathf.Rad2Deg;

		IsShield = (GameObject)Instantiate (shield, GetComponent<Transform> ().position + new Vector3(0, 1, 0), Quaternion.AngleAxis (angle, Vector3.forward));
		IsShield.GetComponent<shield> ().player = gameObject;
		IsShield.SetActive (false);

		IsSword = (GameObject)Instantiate (sword, GetComponent<Transform> ().position + new Vector3(0, 1, 0), Quaternion.identity);
		IsSword.GetComponent<sword>().owner = gameObject.tag;
		IsSword.GetComponent<sword> ().player = gameObject;
		IsSword.SetActive (false);
	}

	
	// Update is called once per frame
	void Update () {
	
	}



	void FixedUpdate() {
		string playerNumber = GetComponent<Player> ().playerNumber;
		Vector2 facing = GetComponent<Player> ().facing;
		shieldtime -= Time.deltaTime;
		if (!IsSword.activeSelf) {
			IsSword.transform.position = gameObject.transform.position;
		}
		if (!IsShield.activeSelf) {
			IsShield.transform.position = gameObject.transform.position;
		}



		


			


		
		if (Input.GetButtonDown (playerNumber + "Ability2") && shieldtime <=0) {
			Debug.Log ("test");
			shieldtime = 3.0f;
			IsShield.SetActive(true);

		}
		
		if (Input.GetButtonDown (playerNumber + "Ability1")) {
			IsSword.SetActive(true);


				
				

			}
		}


	}








