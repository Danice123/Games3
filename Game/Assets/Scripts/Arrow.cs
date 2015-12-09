using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public int damage = 10;
	public int ticksAlive = 30;
	public int rotateSpeed = 100;
	public string owner;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		if (ticksAlive == 0) {
			gameObject.SetActive(false);
		}
		ticksAlive--;

		Vector2 dir = GetComponent<Rigidbody2D> ().velocity;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		var q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag ("Ground")) {
			gameObject.SetActive(false);
		}
		if ((collider.CompareTag ("Left") || collider.CompareTag ("Right")) && !collider.CompareTag(owner)) {
			if (collider.GetComponent<Health>() == null) return;
			if (!Network.isClient) {
				collider.GetComponent<Health>().changeHealth(-damage);
				if (NetworkManager.isNetworkGame) collider.GetComponent<NetworkView> ().RPC("changeHealth", RPCMode.OthersBuffered, -10);
			}
			gameObject.SetActive(false);
		}
	}
}
