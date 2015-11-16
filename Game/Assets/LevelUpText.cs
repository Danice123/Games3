using UnityEngine;
using System.Collections;

public class LevelUpText : MonoBehaviour {

	public int timer = 0;

	// Update is called once per frame
	void Update () {
		timer--;
		if (timer <= 0) {
			timer = 0;
			gameObject.SetActive(false);
		}
	}
}
