using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Respawn : MonoBehaviour {

	List<GameObject> respawnList;
	public bool gameover = false;
	public int timeout = 120;

	// Use this for initialization
	void Start () {
		respawnList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject o in respawnList) {
			o.GetComponent<Player>().respawnTimer--;
			if (o.GetComponent<Player>().respawnTimer <= 0) {
				o.SetActive(true);
				respawnList.Remove(o);
			}
		}
		if (gameover)
			timeout--;
		if (timeout <= 0)
			Application.LoadLevel(0);
	}

	public void addSpawn(GameObject o) {
		respawnList.Add (o);
	}
}
