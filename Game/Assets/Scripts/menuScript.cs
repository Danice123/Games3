using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public void LoadScene(int level)
    {
        Application.LoadLevel(level);
    }

	public void LoadPractice(int ch) {
		PlayerSpawn.playerType = ch;
		Application.LoadLevel(3);

	}
	// Update is called once per frame
	void Update () {
	
	}
}
