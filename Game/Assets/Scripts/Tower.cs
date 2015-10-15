using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

	List<GameObject> attackList;
	public GameObject TowerShot;
	public int delay = 120;
	int Rdelay;
	
	void Start () {
		attackList = new List<GameObject> ();
		Rdelay = delay;
	}

	void FixedUpdate () {
		Rdelay--;
		if (attackList.Count > 0 && attackList [0].gameObject == null) {
			attackList.RemoveAt(0);
			Debug.Log("removed " + GetComponent<Collider>().gameObject.name);
		}
		if (attackList.Count > 0 && attackList [0].GetComponent<Health> () != null && attackList [0].GetComponent<Health> ().health <= 0) {
			attackList.RemoveAt(0);
			Debug.Log("removed " + GetComponent<Collider>().gameObject.name);
		}
		if (attackList.Count > 0 && Rdelay <= 0 && attackList [0] != null) {
			GameObject shot = (GameObject) Instantiate (TowerShot, GetComponent<Transform>().position, Quaternion.identity);
			shot.GetComponent<TowerShot>().target = attackList[0];
			shot.tag = tag;
			Rdelay = delay;
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
		transform.Find ("turretbase").tag = tag;
	}
}
