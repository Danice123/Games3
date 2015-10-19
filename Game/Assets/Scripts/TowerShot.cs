using UnityEngine;
using System.Collections;



public class TowerShot : MonoBehaviour {
	public GameObject persistObject;
	public GameObject target;
	public float speed = 2f;
	bool local = false;


	void FixedUpdate () {
		if (!Network.isClient)
			transform.position = Vector2.MoveTowards(GetComponent<Transform>().position, target.GetComponent<Transform>().position, speed * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (!Network.isClient && (col.CompareTag ("Left") || col.CompareTag ("Right")) && !col.CompareTag (gameObject.tag)) {
			target.GetComponent<Health> ().changeHealth (-10);
			target.GetComponent<NetworkView> ().RPC ("changeHealth", RPCMode.OthersBuffered, -10);
			GetComponent<NetworkView> ().RPC ("kill", RPCMode.OthersBuffered, null);
			kill ();
		} 
	}

	[RPC]
	void kill() {
		DestroyObject (gameObject);
	}
}
