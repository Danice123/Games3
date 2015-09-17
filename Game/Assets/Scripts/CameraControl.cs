using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject tracking;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = GetComponent<Transform> ().position;
		Vector3 newPos = tracking.GetComponent<Transform> ().position;
		GetComponent<Transform> ().position = new Vector3 (newPos.x, pos.y, pos.z);
	}
}
