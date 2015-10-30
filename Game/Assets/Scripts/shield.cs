using UnityEngine;
using System.Collections;

public class shield : MonoBehaviour {


	public int ticksAlive = 30;
	public int rotateSpeed = 800;
	public string owner;
	public GameObject player;
	public p2 ori;
	

	// Use this for initialization
	void Start () {
		//player = GameObject.Find ("secondp");
		Vector2 facing = player.GetComponent<Player> ().facing;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate () {

		Vector2 facing = player.GetComponent<Player> ().facing;
		if (ticksAlive <= 0){
			ticksAlive = 30;
			gameObject.SetActive (false);
		}
		ticksAlive--;
		gameObject.transform.position = new Vector2 (player.transform.position.x +1.5f*facing.x, player.transform.position.y +1.5f*facing.y +1);
		transform.rotation = Quaternion.FromToRotation (new Vector3 (0, 1, 0),new Vector3 ( facing.x, facing.y, 0));
		
		
	}
	
	void OnTriggerEnter2D(Collider2D collider) {


	}
}
