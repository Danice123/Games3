using UnityEngine;
using System.Collections;

public class Defender : MonoBehaviour {
	public GameObject turret;

	void FixedUpdate() {
		if (GetComponent<Health> ().health <= 0){
			Destroy(turret);
			DestroyObject (gameObject);
			GameObject.Find("GameManager").GetComponent<GameManager>().endGame(tag);
		}

	}
}
