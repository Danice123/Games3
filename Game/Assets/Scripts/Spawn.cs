using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : MonoBehaviour {

	public GameObject spawn;
	private int timer = 0;
	List<GameObject> spawnList;

	// Use this for initialization
	void Start () {
		spawnList = new List<GameObject> ();
	}

	void FixedUpdate() {
		if (timer == 600) {
			timer = 0;
		}
		if (timer == 0) {
			Vector3 pos = GetComponent<Transform>().position;
			bool recycle = false;
			if (Network.isServer) {
				foreach (GameObject g in spawnList) {
					if (!g.activeSelf) {
						g.GetComponent<Transform>().position = pos;
						g.GetComponent<NetworkView>().RPC("resetHealth", RPCMode.AllBuffered, null);
						g.GetComponent<NetworkView>().RPC("respawn", RPCMode.AllBuffered, null);
						//g.SetActive(true);
						recycle = true;
						break;
					}
				}
				if (!recycle) spawnList.Add ((GameObject) Network.Instantiate(spawn, pos, Quaternion.identity, 0));
			} else {
				foreach (GameObject g in spawnList) {
					if (!g.activeSelf) {
						g.GetComponent<Transform>().position = pos;
						g.GetComponent<Health>().resetHealth();
						g.SetActive(true);
						recycle = true;
						break;
					}
				}
				if (!recycle) spawnList.Add ((GameObject) Instantiate(spawn, pos, Quaternion.identity));
			}
		}
		timer++;
	}
}
