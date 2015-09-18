using UnityEngine;
using System.Collections;

public class Defender : MonoBehaviour {
	public GameObject turret;

	// Use this for initialization
	void Start () {
		turret = GameObject.Find ("turretf");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate() {
		if (GetComponent<Health> ().health <= 0){
			Destroy(turret);
			DestroyObject (gameObject);
	}
	}
}
