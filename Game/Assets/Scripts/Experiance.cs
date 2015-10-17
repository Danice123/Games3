using UnityEngine;
using System.Collections;

public class Experiance : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D hit) {
		if (hit.gameObject.tag == "Ground") {

		} else {
			Physics2D.IgnoreCollision(hit.collider, GetComponent<Collider2D>());
		}
	}

	[RPC]
	public void kill() {
		DestroyObject (gameObject);
	}
}
