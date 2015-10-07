using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string playerNumber = "1";
	
	public int jumpTimes = 2;
	public float moveSpeed = 1.0f;
	public float jumpSpeed = 1.0f;
	public int respawnTimer = 60;

	public bool canMove = true;

	public Vector2 facing = new Vector2(1, 0);
	public int jumped = 0;

	public int level = 1;
	public int ability1Level = 0;
	public int ability2Level = 0;
	public int ability3Level = 0;
	public int exp = 0;
	public int abilityPoints = 1;
	bool levelupMode = false;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (facing.x > 0) {
			GetComponentsInChildren<Transform>()[1].rotation = Quaternion.AngleAxis(90, Vector3.up);
		} else {
			GetComponentsInChildren<Transform>()[1].rotation = Quaternion.AngleAxis(-90, Vector3.up);
		}
	}

	void FixedUpdate() {
		if (GetComponent<Health> ().health <= 0) {
			respawnTimer = 60 * 1;
			GetComponent<Transform>().position = new Vector2(0, 2);
			GetComponent<Health>().health = GetComponent<Health>().maxHealth;
			GameObject.Find("Respawn").GetComponent<Respawn>().addSpawn(gameObject);
			gameObject.SetActive(false);
		}

		if (Input.GetAxisRaw ("Horizontal" + playerNumber) != 0 || Input.GetAxisRaw ("Vertical" + playerNumber) != 0) {
			facing = new Vector2(Input.GetAxisRaw("Horizontal" + playerNumber), -Input.GetAxisRaw("Vertical" + playerNumber)).normalized;
		}

		Vector2 vel = GetComponent<Rigidbody2D> ().velocity;

		if (canMove || levelupMode) {
			float ha = Input.GetAxisRaw ("Horizontal" + playerNumber) * moveSpeed;

			if (Input.GetButtonDown ("Jump" + playerNumber) && jumped > 0) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, jumpSpeed);
				jumped--;
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, vel.y);
			}
			animator.SetFloat("Speed", Mathf.Abs(ha));
		}
		if (!levelupMode && abilityPoints > 0 && Input.GetButtonDown ("LevelUp" + playerNumber)) {
			levelupMode = true;
		}
		if (levelupMode) {
			if (Input.GetButtonDown (playerNumber + "Ability1")) {
				ability1Level++;
				abilityPoints--;
				levelupMode = false;
			}
			if (Input.GetButtonDown (playerNumber + "Ability2")) {
				ability2Level++;
				abilityPoints--;
				levelupMode = false;
			}
			if (Input.GetButtonDown (playerNumber + "Ability3")) {
				ability3Level++;
				abilityPoints--;
				levelupMode = false;
			}
		}
	}

	void OnCollisionEnter2D (Collision2D hit) {
		if (hit.gameObject.tag == "Ground") {
			jumped = jumpTimes;
		}
	}
}
