using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ranger : MonoBehaviour {

	public GameObject arrow;
	public int arrowSpeed = 10;
	
	public GameObject largeArrow;
	public int largeArrowSpeed = 20;

	private int ticksHeld = -1;
	public int maxTicksHeld = 5;

	private Player player;
	List<GameObject> arrowList;
	List<GameObject> largeArrowList;

	// Use this for initialization
	void Start () {
		player = GetComponent<Player> ();
		player.triangleCooldown = 240;
		player.squareCooldown = 15;
		if ((Network.isServer || Network.isClient) && !GetComponent<NetworkView> ().isMine) {
			Destroy (GetComponent<Rigidbody2D>());
		}
		arrowList = new List<GameObject> ();
		largeArrowList = new List<GameObject> ();
	}

	void FixedUpdate () {
		if (!(Network.isServer || Network.isClient) || GetComponent<NetworkView> ().isMine) {
			NetworkView view = GetComponent<NetworkView> ();
			string playerNumber = player.playerNumber;
			Vector2 facing = player.facing;

			//Ability 1
			if (Input.GetButtonDown (playerNumber + "Ability1") && player.squareCooldownTimer == 0 /* && player.ability1Level > 0*/) {
				float angle = Mathf.Atan2 (facing.y, facing.x) * Mathf.Rad2Deg;
				Vector2 vel = facing * arrowSpeed + new Vector2 (0, 5);
				Vector3 pos = GetComponent<Transform> ().position + new Vector3 (0, 1, 0);
				Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);

				shootArrow(pos, q, new Vector3(vel.x, vel.y, 0));
				view.RPC("shootArrow", RPCMode.OthersBuffered, pos, q, new Vector3(vel.x, vel.y, 0));
				GetComponentInChildren<Animator> ().SetTrigger ("Attack");
				player.squareCooldownTimer = 15;
			}

			//Ability 2
			if (Input.GetButtonDown (playerNumber + "Ability2") && player.triangleCooldownTimer == 0 /* && player.ability2Level > 0*/) {
				ticksHeld = 0;
				player.canMove = false;
				Vector2 vel = GetComponent<Rigidbody2D> ().velocity;
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, vel.y);
				player.triangleCooldownTimer = 240;
			}
			if (ticksHeld >= 0) {
				ticksHeld++;
			
				if (Input.GetButtonUp (playerNumber + "Ability2") || ticksHeld > maxTicksHeld) {
					float angle = Mathf.Atan2 (facing.y, facing.x) * Mathf.Rad2Deg;
					Vector2 vel = facing * largeArrowSpeed;
					Vector3 pos = GetComponent<Transform> ().position + new Vector3 (0, 1, 0);
					Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);

					shootLargeArrow(pos, q, new Vector3(vel.x, vel.y, 0));
					view.RPC("shootLargeArrow", RPCMode.OthersBuffered, pos, q, new Vector3(vel.x, vel.y, 0));
					ticksHeld = -1;
					player.canMove = true;
					GetComponentInChildren<Animator> ().SetTrigger ("Attack");
				}
			}
		}
		GetComponentInChildren<Animator> ().SetBool("isAttacking", player.canMove);
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
	void shootArrow(Vector3 position, Quaternion angle, Vector3 velocity) {
		bool recycle = false;
		foreach (GameObject g in arrowList) {
			if (!g.activeSelf) {
				g.SetActive(true);
				g.GetComponent<Transform>().position = position;
				g.GetComponent<Transform>().rotation = angle;
				g.GetComponent<Rigidbody2D>().velocity = velocity;

				recycle = true;
				break;
			}
		}
		if (!recycle) {
			GameObject a = (GameObject)Instantiate (arrow, position, angle);
			a.GetComponent<Rigidbody2D> ().velocity = velocity;
			a.GetComponent<Arrow> ().owner = gameObject.tag;
			arrowList.Add (a);
		}
	}

	[RPC]
	void shootLargeArrow(Vector3 position, Quaternion angle, Vector3 velocity) {
		bool recycle = false;
		foreach (GameObject g in largeArrowList) {
			if (!g.activeSelf) {
				g.SetActive(true);
				g.GetComponent<Transform>().position = position;
				g.GetComponent<Transform>().rotation = angle;
				g.GetComponent<Rigidbody2D>().velocity = velocity;
				recycle = true;
				break;
			}
		}
		if (!recycle) {
			GameObject a = (GameObject)Instantiate (largeArrow, position, angle);
			a.GetComponent<Rigidbody2D> ().velocity = velocity;
			a.GetComponent<Arrow> ().owner = gameObject.tag;
			largeArrowList.Add (a);
		}
	}

	[RPC]
	void networkSetTag(string tag) {
		this.tag = tag;
	}
}
