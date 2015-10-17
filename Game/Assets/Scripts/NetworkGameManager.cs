using UnityEngine;
using System.Collections;

public class NetworkGameManager : MonoBehaviour {
	
	public GameObject player;
	public Transform player1Spawn;
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
		if (Network.isServer) {
			player = (GameObject)Network.Instantiate (player, player1Spawn.position, Quaternion.identity, 0);
			player.GetComponent<NetworkView> ().RPC ("networkSetTag", RPCMode.AllBuffered, "Left");
		} else {
			player = (GameObject) Network.Instantiate(player, player2Spawn.position, Quaternion.identity, 0);
			player.GetComponent<NetworkView> ().RPC ("networkSetTag", RPCMode.AllBuffered, "Right");
		}
		
		//Setup Camera
		camera.GetComponent<CameraControl> ().tracking = player;

		if (Network.isServer) {
			//Setup Towers
			GameObject gotower;
			gotower = (GameObject)Network.Instantiate (tower, LeftTowerSpawn.position, Quaternion.identity, 0);
			gotower.GetComponent<NetworkView> ().RPC("networkSetTag", RPCMode.AllBuffered, "Left");

			gotower = (GameObject)Network.Instantiate (tower, RightTowerSpawn.position, Quaternion.identity, 0);
			gotower.GetComponent<NetworkView> ().RPC("networkSetTag", RPCMode.AllBuffered, "Right");

			//Setup Spawners
			LeftSpawner = (GameObject)Instantiate (LeftSpawner, player1Spawn.position, Quaternion.identity);
			RightSpawner = (GameObject)Instantiate (RightSpawner, player2Spawn.position, Quaternion.identity);
		}
	}

	[RPC]
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
