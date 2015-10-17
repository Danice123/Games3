using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {

	public bool isLeftMinion = true;
	public float moveSpeed = 1.0f;
	public float jumpSpeed = 1.0f;

	private bool attackMode = false;
	public int attackCooldown = 20;

	public GameObject exp;
	public float expSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		if (Network.isClient) {
			Destroy (GetComponent<Rigidbody2D>());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (!Network.isClient) {
			if (GetComponent<Health> ().health <= 0) {
				spawnExp();
				GetComponent<NetworkView> ().RPC("spawnExp", RPCMode.OthersBuffered, null);
				kill ();
				GetComponent<NetworkView> ().RPC("kill", RPCMode.OthersBuffered, null);
			}
			Vector2 vel = GetComponent<Rigidbody2D> ().velocity;

			if (attackMode) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);

				if (attackCooldown <= 0) {
					if (target == null || !target.activeSelf) {
						attackMode = false;
						return;
					}
					target.GetComponent<Health>().changeHealth(-10);
					target.GetComponent<NetworkView> ().RPC("changeHealth", RPCMode.OthersBuffered, -10);
					attackCooldown = 20;
				}
				if (attackCooldown > 0)
					attackCooldown--;
			} else {
				if (isLeftMinion)
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, vel.y);
				else
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, vel.y);
			}
		}
	}

	private GameObject target;

	void OnTriggerEnter2D ( Collider2D collider) {
		if (!attackMode && !Network.isClient) {
			if ((collider.CompareTag ("Left") || collider.CompareTag ("Right")) && !collider.CompareTag(gameObject.tag)) {
				if (Vector2.Distance(collider.gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().position) > 10) return;
				attackMode = true;
				target = collider.gameObject;
			}
			if (collider.CompareTag("Jump")) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpSpeed);
			}
		}
	}

	void OnTriggerExit2D ( Collider2D collider) {
		if (attackMode && !Network.isClient) {
			if ((collider.CompareTag ("Left") || collider.CompareTag ("Right")) && !collider.CompareTag(gameObject.tag)) {
				attackMode = false;
			}
		}
	}

	[RPC]
	void kill() {
		DestroyObject (gameObject);
	}

	[RPC]
	void spawnExp() {
		GameObject a = (GameObject)Instantiate (exp, GetComponent<Transform> ().position, Quaternion.identity);
		if (tag == "Left")
			a.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-0.5f, 0.5f) * expSpeed;
		else
			a.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.5f, 0.5f) * expSpeed;
	}
}
