using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

	List<GameObject> attackList;
	public GameObject TowerShot;
	public int delay = 120;
	int Rdelay;
	public GameObject cannon;
	public GameObject crystal;
	public Transform cannon_point;
	public Transform crystal_point;
	
	void Start () {
		attackList = new List<GameObject> ();
		Rdelay = delay;
	}

	void FixedUpdate () {
		if (tag == "Right") {
			crystal.SetActive(true);
			cannon.SetActive(false);
		}
		if (!Network.isClient) {
			Rdelay--;
			if (attackList.Count > 0 && attackList [0].gameObject == null) {
				attackList.RemoveAt (0);
				Debug.Log ("removed " + GetComponent<Collider> ().gameObject.name);
			}
			if (attackList.Count > 0 && attackList [0].GetComponent<Health> () != null && attackList [0].GetComponent<Health> ().health <= 0) {
				attackList.RemoveAt (0);
				Debug.Log ("removed " + GetComponent<Collider> ().gameObject.name);
			}
			if (attackList.Count > 0 && Rdelay <= 0 && attackList [0] != null) {
				//GetComponent<Animator>().SetBool("shoot", true);
				GameObject shot;
				Transform point;
				if (tag == "Right")
					point = crystal_point;
				else
					point = cannon_point;
				if (Network.isServer) {
					shot = (GameObject)Network.Instantiate (TowerShot, point.position, Quaternion.identity, 0);
				} else {
					shot = (GameObject)Instantiate (TowerShot, point.position, Quaternion.identity);
				}
				shot.GetComponent<TowerShot> ().target = attackList [0];
				shot.tag = tag;
				Rdelay = delay;
			}
		}
	}

	void OnTriggerEnter2D ( Collider2D collider) {
		if ((collider.CompareTag ("Left") || collider.CompareTag ("Right")) && !collider.CompareTag(gameObject.tag)) {
			attackList.Add(collider.gameObject);
			Debug.Log("added " + collider.gameObject.name);
		}
	}
	
	void OnTriggerExit2D ( Collider2D collider) {
		if ((collider.CompareTag ("Left") || collider.CompareTag ("Right")) && !collider.CompareTag(gameObject.tag)) {
			attackList.Remove(collider.gameObject);
			Debug.Log("removed " + collider.gameObject.name);
		}
	}

	[RPC]
	public void networkSetTag(string tag) {
		this.tag = tag;
		transform.FindChild ("turretbase").tag = tag;
	}

	[RPC]
	public void kill() {
		DestroyObject (gameObject);
	}
}
