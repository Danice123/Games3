using UnityEngine;
using System.Collections;

public class spawnbb : MonoBehaviour {

	public Transform spawnloc;
	public GameObject bb;
	public float spawntime = 2f;
	public float timer;
	public GameObject turret;
	public CantoPlayer tracker;
	public GameObject target;


	// Use this for initialization
	void Start () {
		timer = spawntime;

	
	}

	void FixedUpdate(){
		timer -= Time.deltaTime;
		turret = GameObject.FindWithTag("turret");
		tracker = turret.GetComponent<CantoPlayer>();
		target = tracker.player;



	}
	void OnTriggerStay2D (Collider2D col) 
	{
		if(col.gameObject == target && timer <= 0)
		{
			timer = spawntime;
			Instantiate (bb, spawnloc.position, Quaternion.identity);

		}
	}

}
