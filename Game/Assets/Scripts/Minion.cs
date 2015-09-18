using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {

	public bool isLeftMinion = true;
	public float moveSpeed = 1.0f;

	private bool attackMode = false;
	public int attackCooldown = 20;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (GetComponent<Health>().health <= 0)
			DestroyObject (gameObject);
		Vector2 vel = GetComponent<Rigidbody2D> ().velocity;

		if (attackMode) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2(0, 0);

			if (attackCooldown <= 0) {
				if (target == null) {
					attackMode = false;
					return;
				}
				target.GetComponent<Health>().health -= 10;
				attackCooldown = 20;
			}
			if (attackCooldown > 0) attackCooldown--;
		} else {
			if (isLeftMinion)
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, vel.y);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, vel.y);
		}
	}

	private GameObject target;

	void OnTriggerEnter2D ( Collider2D collider) {
		if (!attackMode) {
			if ((collider.CompareTag ("Left") || collider.CompareTag ("Right")) && !collider.CompareTag(gameObject.tag)) {
				if (Vector2.Distance(collider.gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().position) > 10) return;
				attackMode = true;
				target = collider.gameObject;
			}
		}
	}

	void OnTriggerExit2D ( Collider2D collider) {
		if (attackMode) {
			if ((collider.CompareTag ("Left") || collider.CompareTag ("Right")) && !collider.CompareTag(gameObject.tag)) {
				attackMode = false;
			}
		}
	}
}
