using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LocalStart : MonoBehaviour {
	
	public GameObject canvas;
	public GameObject p2Select;

	public GameObject ranger;
	public GameObject warrior;
	public GameObject mage;
	public GameManager gm;

	public void playerOneChooseCharacter(int character) {
		switch (character) {
		case 0:
			gm.player1 = ranger;
			break;
		case 1:
			gm.player1 = warrior;
			break;
		case 2:
			gm.player1 = mage;
			break;
		}
		canvas.GetComponent<EventSystem>().SetSelectedGameObject (p2Select);
	}

	public void playerTwoChooseCharacter(int character) {
		switch (character) {
		case 0:
			gm.player2 = ranger;
			break;
		case 1:
			gm.player2 = warrior;
			break;
		case 2:
			gm.player2 = mage;
			break;
		}
		canvas.SetActive (false);
		gm.StartGame ();
	}
}
