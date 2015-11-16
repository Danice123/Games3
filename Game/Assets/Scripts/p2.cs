using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class p2 : MonoBehaviour {


	public GameObject sword;
	public GameObject blade1;
	public GameObject blade2;
	public GameObject IsSword;
	public float swordtime = 1f;
	public GameObject shield;
	public float shieldtime = 3f;
	public GameObject IsShield;
	private Player player;
	public float bladetime = 4f;


	


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

		player.triangleCooldown = 60;
		player.squareCooldown = 10;


	}

	
	// Update is called once per frame
	void Update () {
	
	}



	void FixedUpdate() {
		if (!(Network.isServer || Network.isClient) || GetComponent<NetworkView> ().isMine) {
			NetworkView view = GetComponent<NetworkView> ();
			string playerNumber = GetComponent<Player> ().playerNumber;
			Vector2 facing = GetComponent<Player> ().facing;
			shieldtime -= Time.deltaTime;
			swordtime -= Time.deltaTime;
			if (!IsSword.activeSelf) {
				IsSword.transform.position = gameObject.transform.position;
			}
			if (!IsShield.activeSelf) {
				IsShield.transform.position = gameObject.transform.position;
			}



		


			


			
			if (Input.GetButtonDown (playerNumber + "Ability2") && player.triangleCooldownTimer == 0) {

				shieldtime = 3.0f;

				SetShield();
				view.RPC("SetShield", RPCMode.OthersBuffered, null);
				
				player.triangleCooldownTimer = 60;


			}

			if (Input.GetButtonDown (playerNumber + "Ability3") && bladetime <= 0) {


				
			}
			
			if (Input.GetButtonDown (playerNumber + "Ability1") && player.squareCooldownTimer == 0) {
				swordtime = 1.0f;
				SetSword();
				view.RPC("SetSword", RPCMode.OthersBuffered, null);

				player.squareCooldownTimer = 10;


				
				

			}
		}

		player.triangleCooldownTimer--;
		if (player.triangleCooldownTimer < 0) {
			player.triangleCooldownTimer = 0;
		}
		player.squareCooldownTimer--;
		if (player.squareCooldownTimer < 0) {
			player.squareCooldownTimer = 0;
		}

	}


	[RPC]
	void SetShield() {
		
		IsShield.SetActive (true);
		
	}


	[RPC]
	void SetSword() {

		IsSword.SetActive (true);

	}

	[RPC]
	void networkSetTag(string tag) {
		this.tag = tag;
	}



	}








