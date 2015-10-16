using UnityEngine;
using System.Collections;

public class TowerShot : MonoBehaviour {

	public GameObject target;
	public float speed = 2f;
	
	void FixedUpdate () {
		if (!Network.isClient && target == null)
			Destroy (gameObject);
		transform.position = Vector2.MoveTowards(GetComponent<Transform>().position, target.GetComponent<Transform>().position, speed * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (!Network.isClient && (col.CompareTag ("Left") || col.CompareTag ("Right")) && !col.CompareTag(gameObject.tag)) {
			col.gameObject.GetComponent<Health>().health -= 25;
			Destroy(gameObject);
		}
	}
}
