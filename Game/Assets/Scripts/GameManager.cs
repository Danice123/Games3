using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject player1;
	public Transform player1Spawn;
	public GameObject player2;
	public Transform player2Spawn;

	public GameObject camera;

	public Transform LeftTowerSpawn;
	public Transform RightTowerSpawn;
	public GameObject tower;

	public GameObject LeftSpawner;
	public GameObject RightSpawner;

	public GameObject GameOverCanvas;
	bool gameOver = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver && (Input.GetButton("Jump1") || Input.GetButton("Jump2"))) {
			Application.LoadLevel(0);
			Debug.Log("Game is Quit");
		}
	}

	public void StartGame() {
		//Create Players
		player1 = (GameObject) Instantiate (player1, player1Spawn.position, Quaternion.identity);
		player1.tag = "Left";

		player2 = (GameObject) Instantiate (player2, player2Spawn.position, Quaternion.identity);
		player2.GetComponent<Player> ().playerNumber = "2";
		player2.tag = "Right";

		//Setup Camera
		camera.GetComponent<CameraControl> ().tracking = player1;
		camera.GetComponent<CameraControl> ().tracking2 = player2;

		//Setup Towers
		GameObject gotower;
		gotower = (GameObject)Instantiate (tower, LeftTowerSpawn.position + new Vector3(0, 5, 0), Quaternion.identity);

		gotower = (GameObject)Instantiate (tower, RightTowerSpawn.position + new Vector3(0, 5, 0), Quaternion.identity);
		gotower.GetComponent<Tower> ().networkSetTag ("Right");

		//Setup Spawners
		LeftSpawner = (GameObject) Instantiate (LeftSpawner, player1Spawn.position, Quaternion.identity);
		RightSpawner = (GameObject) Instantiate (RightSpawner, player2Spawn.position, Quaternion.identity);
	}

	public void endGame(string side) {
		gameOver = true;
		GameOverCanvas.SetActive (true);
		if (side == "Right") {
			GameOverCanvas.transform.FindChild("Left").gameObject.SetActive(true);
		} else {
			GameOverCanvas.transform.FindChild("Right").gameObject.SetActive(true);
		}
	}
}
