﻿using UnityEngine;
using System.Collections;

public class shield : MonoBehaviour {


	public int ticksAlive = 20;
	public int rotateSpeed = 800;
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
		if (ticksAlive == 0)
			DestroyObject (gameObject);
		ticksAlive--;
		gameObject.transform.position = new Vector2 (gameObject.transform.position.x, player.transform.position.y);
		Vector2 dir = GetComponent<Rigidbody2D> ().velocity;
		
		
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag ("Ground")) {
			DestroyObject(gameObject);
		}

	}
}