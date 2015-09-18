using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	public int timer = 120;
	public GameObject fireball;
	public float speed = 1;
	public GameObject target;
	public GameObject text;

	private int realTimer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (realTimer == 0) {
			Vector3 pos = GetComponent<Transform>().position;
			Vector3 targetPos = target.GetComponent<Transform>().position;
			GameObject a = (GameObject)Instantiate (fireball, GetComponent<Transform> ().position, Quaternion.identity);
			a.GetComponent<Rigidbody2D> ().velocity = new Vector2(targetPos.x, targetPos.y) - new Vector2(pos.x, pos.y);
			a.GetComponent<Arrow>().owner = gameObject.tag;
			realTimer = timer;
		}
		realTimer--;

		if (GetComponent<Health> ().health <= 0){
			DestroyObject (gameObject);
			text.SetActive(true);
			GameObject.Find("Respawn").GetComponent<Respawn>().gameover = true;
		}
	}
}
