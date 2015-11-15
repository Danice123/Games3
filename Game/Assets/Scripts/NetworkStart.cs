using UnityEngine;
using System.Collections;

public class NetworkStart : MonoBehaviour {
	
	public GameObject canvas;
	
	public GameObject ranger;
	public GameObject mage;
	public GameObject gm;
	
	public void playerChooseCharacter(int character) {
		switch (character) {
		case 0:
			gm.GetComponent<NetworkGameManager>().player = ranger;
			break;
		case 1:
			return;
			break;
		case 2:
			gm.GetComponent<NetworkGameManager>().player = mage;
			break;
		}
		canvas.SetActive (false);
		gm.GetComponent<NetworkGameManager>().StartGame ();
	}
}
