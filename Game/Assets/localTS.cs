using UnityEngine;
using System.Collections;

public class localTS : MonoBehaviour {
	
	public GameObject target;
	public float speed = 2f;
	
	void FixedUpdate () {
			transform.position = Vector2.MoveTowards(GetComponent<Transform>().position, target.GetComponent<Transform>().position, speed * Time.deltaTime);
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (!Network.isClient && (col.CompareTag ("Left") || col.CompareTag ("Right")) && !col.CompareTag(gameObject.tag)) {
			target.GetComponent<Health>().changeHealth(-10);
			target.GetComponent<NetworkView> ().RPC("changeHealth", RPCMode.OthersBuffered, -10);
			GetComponent<NetworkView> ().RPC("kill", RPCMode.OthersBuffered, null);
			kill ();
		}
	}
	

	void kill() {
		DestroyObject (gameObject);
	}
}
