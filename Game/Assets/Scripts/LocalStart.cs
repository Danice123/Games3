﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LocalStart : MonoBehaviour {
	
	public GameObject canvas;
	public GameObject p2Select;

	public GameObject ranger;
	public GameManager gm;

	public void playerOneChooseCharacter(int character) {
		switch (character) {
		case 0:
			gm.player1 = ranger;
			break;
		case 1:
			break;
		case 2:
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
			break;
		case 2:
			break;
		}
		canvas.SetActive (false);
		gm.StartGame ();
	}
}