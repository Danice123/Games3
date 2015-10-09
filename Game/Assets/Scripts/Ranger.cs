using UnityEngine;
using System.Collections;

public class Ranger : MonoBehaviour {

	public GameObject arrow;
	public int arrowSpeed = 10;
	
	public GameObject largeArrow;
	public int largeArrowSpeed = 20;

	private int ticksHeld = -1;
	public int maxTicksHeld = 5;

	private Player player;

	// Use this for initialization
	void Start () {
		player = GetComponent<Player> ();
	}

	void Update () {
		//Vector2 facing = GetComponent<Player> ().facing;

		//GameObject bow = GameObject.Find ("longbow");
		//float angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
		//var q = Quaternion.AngleAxis(angle + 45.0f, Vector3.forward);
		//bow.GetComponent<Transform> ().rotation = q;


		//Vector2 newPos = GetComponent<Transform> ().position;
		//newPos += facing * 0.5f;
		//bow.GetComponent<Transform>().position = new Vector3(newPos.x, newPos.y, 0);
	}

	void FixedUpdate () {
		if (GetComponent<NetworkView> ().isMine) {
			NetworkView view = GetComponent<NetworkView> ();
			string playerNumber = player.playerNumber;
			Vector2 facing = player.facing;
			if (Input.GetButtonDown (playerNumber + "Ability1")/* && player.ability1Level > 0*/) {
				float angle = Mathf.Atan2 (facing.y, facing.x) * Mathf.Rad2Deg;
				Vector2 vel = facing * arrowSpeed + new Vector2 (0, 5);
				view.RPC("shootArrow", RPCMode.All, 
				         GetComponent<Transform> ().position + new Vector3 (0, 1, 0), 
				         Quaternion.AngleAxis (angle, Vector3.forward),
				         new Vector3(vel.x, vel.y, 0));

				GetComponentInChildren<Animator> ().SetTrigger ("Attack");
			}
		
			if (Input.GetButtonDown (playerNumber + "Ability2")/* && player.ability2Level > 0*/) {
				ticksHeld = 0;
				player.canMove = false;

				Vector2 vel = GetComponent<Rigidbody2D> ().velocity;
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, vel.y);
			}
		
			if (ticksHeld >= 0) {
				ticksHeld++;
			
				if (Input.GetButtonUp (playerNumber + "Ability2") || ticksHeld > maxTicksHeld) {
					GameObject a = (GameObject)Network.Instantiate (largeArrow, GetComponent<Transform> ().position + new Vector3 (0, 1, 0), Quaternion.identity, 0);
					a.GetComponent<Rigidbody2D> ().velocity = facing * largeArrowSpeed;
					a.GetComponent<Arrow> ().owner = gameObject.tag;
					ticksHeld = -1;
					player.canMove = true;
					GetComponentInChildren<Animator> ().SetTrigger ("Attack");
				}
			}
		}
		GetComponentInChildren<Animator> ().SetBool("isAttacking", player.canMove);
	}

	[RPC]
	void shootArrow (Vector3 position, Quaternion angle, Vector3 velocity) {
		GameObject a = (GameObject)Network.Instantiate (arrow, position, angle, 0);
		a.GetComponent<Rigidbody2D> ().velocity = velocity;
		a.GetComponent<Arrow> ().owner = gameObject.tag;
	}
}
