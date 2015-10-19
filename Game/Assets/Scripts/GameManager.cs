using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject player1;
	public Transform player1Spawn;
	public GameObject player2;
	public Transform player2Spawn;

	public GameObject camera;

    //Use this to access all pertinent info from the menus
    public GameObject persistObject;

	public Transform LeftTowerSpawn;
	public Transform RightTowerSpawn;
	public GameObject tower;
    public GameObject ranger;
    public GameObject warrior;
    public GameObject mage;
	public GameObject LeftSpawner;
	public GameObject RightSpawner;
    public GameObject HUD1, HUD2;

	public GameObject GameOverCanvas;
	bool gameOver = false;

	// Use this for initialization
	void Start () {
        persistObject = GameObject.Find("persistObject");
        switch (persistObject.GetComponent<menuObjectScript>().player1)
        {
            case 0:
                player1 = ranger;
                break;
            case 1:
                player1 = warrior;
                break;
            case 2:
                player1 = mage;
                break;
        }
        switch (persistObject.GetComponent<menuObjectScript>().player2)
        {
            case 0:
                player2 = ranger;
                break;
            case 1:
                player2 = warrior;
                break;
            case 2:
                player2 = mage;
                break;
        }
        StartGame();
    }
	
	// Update is called once per frame
	void Update () {
		if (gameOver && (Input.GetButton("Jump1") || Input.GetButton("Jump2"))) {
			Application.LoadLevel(0);
			Debug.Log("Game is Quit");
		}
		if (player1 != null && player1.GetComponent<Player> ().isDead) {
			player1.GetComponent<Player>().respawnTimer--;
			if (player1.GetComponent<Player>().respawnTimer <= 0) {
				player1.SetActive(true);
				player1.GetComponent<Player>().isDead = false;
			}
		}
		if (player2 != null && player2.GetComponent<Player> ().isDead) {
			player2.GetComponent<Player>().respawnTimer--;
			if (player2.GetComponent<Player>().respawnTimer <= 0) {
				player2.SetActive(true);
				player2.GetComponent<Player>().isDead = false;
			}
		}
	}

	public void StartGame() {
		//Create Players
		player1 = (GameObject) Instantiate (player1, player1Spawn.position, Quaternion.identity);
		player1.tag = "Left";
		player1.GetComponent<Player> ().respawnPosition = player1Spawn;



		player2 = (GameObject) Instantiate (player2, player2Spawn.position, Quaternion.identity);
		player2.GetComponent<Player> ().playerNumber = "2";
		player2.tag = "Right";
		player2.GetComponent<Player> ().respawnPosition = player2Spawn;


        //HUDS
        HUD1.GetComponent<cooldownView>().player = player1;
        HUD2.GetComponent<cooldownView>().player = player2;

        //Setup Camera
        camera.GetComponent<CameraControl> ().tracking = player1;
		camera.GetComponent<CameraControl> ().tracking2 = player2;

		//Setup Towers
		GameObject gotower;
		gotower = (GameObject)Instantiate (tower, LeftTowerSpawn.position, Quaternion.identity);

		gotower = (GameObject)Instantiate (tower, RightTowerSpawn.position, Quaternion.identity);
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
