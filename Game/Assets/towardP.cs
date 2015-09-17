using UnityEngine;
using System.Collections;

public class towardP : MonoBehaviour {

	public Transform target;
	public float speed = 2f;
	private CantoPlayer tracker;
	public GameObject turret;


	void Start () {
		turret = GameObject.FindWithTag("turret");
		tracker = turret.GetComponent<CantoPlayer>();
		target = tracker.target;
		
	}


	// Update is called once per frame
	void FixedUpdate () {

		transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);


	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if( col.gameObject.tag == "Right")
		{
			col.gameObject.GetComponent<Health>().health -= 25;
			Destroy(gameObject);
		}
	}

}
