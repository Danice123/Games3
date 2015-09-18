using UnityEngine;
using System.Collections;


public class CantoPlayer : MonoBehaviour {

	public Transform target;
	public GameObject player;
	bool attacking;
	public string team;


	void Start () {
		attacking = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (attacking) {
			if (player == null) {

				attacking = false;
				return;

			}
		
			Quaternion rotation = Quaternion.LookRotation
			(target.transform.position - transform.position, transform.TransformDirection (Vector3.up));
			transform.rotation = new Quaternion (0, 0, rotation.z, rotation.w);

		}
	
	}

	void OnTriggerStay2D (Collider2D col) {
	
		if (!attacking) {
			if ((col.CompareTag ("Left") || col.CompareTag ("Right")) && !col.CompareTag(team)) {
				attacking = true;
				player = col.gameObject;
				target = player.transform;
			}
		}

			

	}
	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject == player) {

			player = null;
		}


	}
}


