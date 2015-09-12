using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject spawn;
	private int timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (timer == 600) {
			timer = 0;
		}
		if (timer == 0) {
			Vector3 pos = GetComponent<Transform>().position;
			Instantiate(spawn, new Vector3 (pos.x, pos.y, pos.z), Quaternion.identity);
		}
		timer++;
	}
}
