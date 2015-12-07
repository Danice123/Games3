using UnityEngine;
using System.Collections;

public class Defender : MonoBehaviour {
	public GameObject turret;

	void FixedUpdate() {
		if (!Network.isClient && GetComponent<Health> ().health <= 0){
			if (Network.isServer)
				GameObject.Find("NetworkManager").GetComponent<NetworkView> ().RPC("endGame", RPCMode.AllBuffered, tag);
			else
				GameObject.Find("GameManager").GetComponent<GameManager>().endGame(tag);

			turret.GetComponent<NetworkView> ().RPC("kill", RPCMode.OthersBuffered, null);
			turret.GetComponent<Tower>().kill();
		}
	}
}
