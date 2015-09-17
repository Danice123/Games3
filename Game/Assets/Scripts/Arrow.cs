using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public int damage = 10;
	public int ticksAlive = 30;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		if (ticksAlive == 0)
			DestroyObject (gameObject);
		ticksAlive--;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag ("Ground")) {
			DestroyObject(gameObject);
		}
		if (collider.CompareTag ("Right")) {
			collider.gameObject.GetComponent<Health>().health -= damage;
			DestroyObject(gameObject);
		}
	}
}
