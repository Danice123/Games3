using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject tracking;
	public GameObject tracking2;
	public bool trackTwo = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = GetComponent<Transform> ().position;
		if (trackTwo) {
			Vector3 t1p = tracking.GetComponent<Transform> ().position;
			Vector3 t2p = tracking2.GetComponent<Transform> ().position;
			float dif = Mathf.Abs(t1p.x - t2p.x);
			if (dif > 10)
				GetComponent<Camera>().orthographicSize = dif / 2.0f;
			if (t1p.x > t2p.x)
				GetComponent<Transform> ().position = new Vector3 (t2p.x + dif / 2, pos.y, pos.z);
			else
				GetComponent<Transform> ().position = new Vector3 (t1p.x + dif / 2, pos.y, pos.z);
			return;
		}
		Vector3 newPos = tracking.GetComponent<Transform> ().position;
		GetComponent<Transform> ().position = new Vector3 (newPos.x, pos.y, pos.z);
	}
}
