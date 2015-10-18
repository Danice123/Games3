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
		if (tracking == null)
			return;
		Vector3 pos = GetComponent<Transform> ().position;
		if (trackTwo) {
			Vector3 t1p = tracking.GetComponent<Transform> ().position;
			Vector3 t2p = tracking2.GetComponent<Transform> ().position;
			float dif = Mathf.Abs(t1p.x - t2p.x);
			if (dif > 40)
				GetComponent<Camera>().orthographicSize = dif / 4.0f;
			float dif2 = Mathf.Abs(t1p.y - t2p.y);
			if (t1p.x > t2p.x)
				if (t1p.y > t2p.y)
					GetComponent<Transform> ().position = new Vector3 (t2p.x + dif / 2, t2p.y + dif2 / 2, pos.z);
				else
					GetComponent<Transform> ().position = new Vector3 (t2p.x + dif / 2, t1p.y + dif2 / 2, pos.z);
			else
				if (t1p.y > t2p.y)
					GetComponent<Transform> ().position = new Vector3 (t1p.x + dif / 2, t2p.y + dif2 / 2, pos.z);
				else
					GetComponent<Transform> ().position = new Vector3 (t1p.x + dif / 2, t1p.y + dif2 / 2, pos.z);
			return;
		}
		Vector3 newPos = tracking.GetComponent<Transform> ().position;
		GetComponent<Transform> ().position = new Vector3 (newPos.x, newPos.y, pos.z);
	}
}
