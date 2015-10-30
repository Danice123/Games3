using UnityEngine;
using System.Collections;

public class sword : MonoBehaviour {

	public int damage = 10;
	public int ticksAlive = 20;

	public string owner;
	public GameObject player;
	public p2 ori;


	// Use this for initialization
	void Start () {
		//player = GameObject.Find ("secondp");
		Vector2 facing = player.GetComponent<Player> ().facing;
		transform.rotation = Quaternion.FromToRotation (new Vector3 (0, 1, 0),new Vector3 ( facing.x, facing.y, 0));

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		Vector2 facing = player.GetComponent<Player> ().facing;
		if (ticksAlive == 0) {
			ticksAlive = 20;
			gameObject.SetActive (false);
		}
		ticksAlive--;
		gameObject.transform.position = new Vector2 (player.transform.position.x +1*facing.x, player.transform.position.y +1*facing.y +1);
		transform.rotation = Quaternion.FromToRotation (new Vector3 (0, 1, 0),new Vector3 ( facing.x, facing.y, 0));



	}
	
	void OnTriggerEnter2D(Collider2D collider) {

		if ((collider.CompareTag ("Left") || collider.CompareTag ("Right")) && !collider.CompareTag(owner)) {
			collider.gameObject.GetComponent<Health>().health -= damage;
			ticksAlive = 20;
			gameObject.SetActive(false);
		}
	}
}


		
		
		



