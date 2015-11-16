using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p3 : MonoBehaviour
{
	
    public GameObject frostBall;
    public GameObject fireBall;
    const float FROSTBALL_SPEED = 15.0f;
    const float FIREBALL_SPEED = 20.0f;
    private Player player;

	List<GameObject> fireList;
	List<GameObject> iceList;


    // Use this for initialization
    void Start()
    {

        player = GetComponent<Player>();
		player.abilityPoints = 1;
        player.triangleCooldown = 240;
		player.squareCooldown = 15;
		if ((Network.isServer || Network.isClient) && !GetComponent<NetworkView> ().isMine) {
			Destroy (GetComponent<Rigidbody2D>());
		}
		fireList = new List<GameObject> ();
		iceList = new List<GameObject> ();
    }

    void FixedUpdate()
    {
		if (!(Network.isServer || Network.isClient) || GetComponent<NetworkView> ().isMine) {
			NetworkView view = GetComponent<NetworkView> ();
			Vector2 facing = player.facing;
			string playerNumber = GetComponent<Player> ().playerNumber;
			//Ability 1
			if (Input.GetButtonDown (playerNumber + "Ability1") && player.squareCooldownTimer == 0) {
				for (int b = 0; b <= player.ability1Level; b++) {
					float angle = Mathf.Atan2 (facing.y, facing.x) * Mathf.Rad2Deg;
					shootIce(GetComponent<Transform> ().position + new Vector3 (0, 1, 0), Quaternion.AngleAxis (angle, Vector3.forward), facing * FROSTBALL_SPEED + new Vector2 (0, 5));
					view.RPC("shootIce", RPCMode.OthersBuffered, GetComponent<Transform> ().position + new Vector3 (0, 1, 0), Quaternion.AngleAxis (angle, Vector3.forward), (Vector3) (facing * FROSTBALL_SPEED + new Vector2 (0, 5)));
					GetComponentInChildren<Animator> ().SetTrigger ("Attack");
					player.squareCooldownTimer = 15;
				}
			}
			//Ability 2
			if (Input.GetButtonDown (playerNumber + "Ability2") && player.triangleCooldownTimer == 0) {
				float angle = Mathf.Atan2 (facing.y, facing.x) * Mathf.Rad2Deg;
				for (int b = 0; b< 3 + 2 * player.ability2Level; b++) {
					shootFire(GetComponent<Transform> ().position + new Vector3 (0, 1, 0), Quaternion.AngleAxis (angle, Vector3.forward), facing * FIREBALL_SPEED);
					view.RPC("shootFire", RPCMode.OthersBuffered, GetComponent<Transform> ().position + new Vector3 (0, 1, 0), Quaternion.AngleAxis (angle, Vector3.forward), (Vector3) (facing * FIREBALL_SPEED));
				}
				GetComponentInChildren<Animator> ().SetTrigger ("Attack");
				player.triangleCooldownTimer = 240;
			} else {
				player.triangleCooldownTimer--;
				if (player.triangleCooldownTimer < 0) {
					player.triangleCooldownTimer = 0;
				}
				player.squareCooldownTimer--;
				if (player.squareCooldownTimer < 0) {
					player.squareCooldownTimer = 0;
				}
			}
		}
    }

	[RPC]
	void shootIce(Vector3 position, Quaternion angle, Vector3 velocity) {
		bool recycle = false;
		foreach (GameObject g in iceList) {
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
			GameObject a = (GameObject)Instantiate (frostBall, position, angle);
			a.GetComponent<Rigidbody2D> ().velocity = velocity;
			a.GetComponent<frostBall> ().owner = gameObject.tag;
			iceList.Add (a);
		}
	}

	[RPC]
	void shootFire(Vector3 position, Quaternion angle, Vector3 velocity) {
		bool recycle = false;
		foreach (GameObject g in fireList) {
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
			GameObject a = (GameObject)Instantiate (fireBall, position, angle);
			a.GetComponent<Rigidbody2D> ().velocity = velocity;
			a.GetComponent<fireball> ().owner = gameObject.tag;
			fireList.Add (a);
		}
	}

	[RPC]
	void networkSetTag(string tag) {
		this.tag = tag;
	}
}
