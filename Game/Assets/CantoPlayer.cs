using UnityEngine;
using System.Collections;


public class CantoPlayer : MonoBehaviour {

	public Transform target;
	public GameObject player;


	void Start () {
		player = GameObject.FindWithTag("P1");
		target = player.transform;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (player == null) {
			player = GameObject.FindWithTag("P1");
			target = player.transform;

		}
		Quaternion rotation = Quaternion.LookRotation
			(target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
		transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

	
	}

	void OnTriggerStay2D (Collider2D col) {
	
		if (col.gameObject.tag == "Right") {
			player = col.gameObject;
			target = player.transform;
		}

			
		else if (col.gameObject.tag == "P1" && (Vector2.Distance(gameObject.transform.position, player.transform.position)) > (Vector2.Distance(gameObject.transform.position,col.gameObject.transform.position))) 
		{
			player = col.gameObject;
			target = player.transform;
		}
	}
}


