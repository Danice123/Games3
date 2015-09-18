using UnityEngine;
using System.Collections;

public class Ranger : MonoBehaviour {

	public GameObject arrow;
	public int arrowSpeed = 10;
	
	public GameObject largeArrow;
	public int largeArrowSpeed = 20;

	private int ticksHeld = -1;
	public int maxTicksHeld = 5;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		Vector2 facing = GetComponent<Player> ().facing;
		if (Input.GetButtonDown ("X")) {
			float angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
			
			GameObject a = (GameObject) Instantiate(arrow, GetComponent<Transform>().position, Quaternion.AngleAxis(angle, Vector3.forward));
			a.GetComponent<Rigidbody2D>().velocity = facing * arrowSpeed + new Vector2(0, 5);
			a.GetComponent<Arrow>().owner = gameObject.tag;
		}
		
		if (Input.GetButtonDown ("Y")) {
			ticksHeld = 0;
			GetComponent<Player>().canMove = false;
		}
		
		if (ticksHeld >= 0) {
			ticksHeld++;
			
			if (Input.GetButtonUp ("Y") || ticksHeld > maxTicksHeld) {
				GameObject a = (GameObject)Instantiate (largeArrow, GetComponent<Transform> ().position, Quaternion.identity);
				a.GetComponent<Rigidbody2D> ().velocity = facing * largeArrowSpeed;
				a.GetComponent<Arrow> ().owner = gameObject.tag;
				ticksHeld = -1;
				GetComponent<Player>().canMove = true;
			}
		}
	}
}
