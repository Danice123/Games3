using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {

	public GameObject ranger;
	public GameObject warrior;

	public static int playerType;

	// Use this for initialization
	void Start () {
		GameObject g = null;
		if (playerType == 0) {
			g = (GameObject) Instantiate(ranger, new Vector3(0, 2), Quaternion.identity);
		}
		if (playerType == 1) {
			g = (GameObject) Instantiate(warrior, new Vector3(0, 2), Quaternion.identity);
		}
		GameObject.Find ("Camera").GetComponent<CameraControl>().tracking = g;
		GameObject.Find ("Boss").GetComponent<Boss> ().target = g;
	}
}
