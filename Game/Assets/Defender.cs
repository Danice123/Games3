using UnityEngine;
using System.Collections;

public class Defender : MonoBehaviour {
	public GameObject turret;
	public GameObject text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void FixedUpdate() {
		if (GetComponent<Health> ().health <= 0){
			Destroy(turret);
			DestroyObject (gameObject);
			text.SetActive(true);
			GameObject.Find("Respawn").GetComponent<Respawn>().gameover = true;
		}

	}
}
