using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string playerNumber = "1";
	
	public int jumpTimes = 2;
	public float moveSpeed = 1.0f;
	public float jumpSpeed = 1.0f;
	public bool isDead = false;
	public int respawnTimer = 60;
	public Transform respawnPosition;

	public bool canMove = true;
    public float triangleCooldownTimer, triangleCooldown;
	public Vector2 facing = new Vector2(1, 0);
	public int jumped = 0;

	public float slowTimer;
	public float stunTimer;

	public int level = 1;
	public int ability1Level = 0;
	public int ability2Level = 0;
	public int ability3Level = 0;
	public int exp = 0;
	public int abilityPoints = 1;
    public int max_exp = 100;
	public float squareCooldown, squareCooldownTimer;
    public float originalMoveSpeed;
	bool levelupMode = false;
	public GameObject levelUpText;

	public GameObject model;

	private Animator animator;
    
	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator> ();
        originalMoveSpeed = moveSpeed;
	}
	
    public void setcooldown(int cd)
    {
        triangleCooldown = cd;
        triangleCooldownTimer = cd;
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
		if ((!(Network.isServer || Network.isClient) || GetComponent<NetworkView> ().isMine) && GetComponent<Health> ().health <= 0) {
			respawnTimer = 60 * level;
			isDead = true;
			GetComponent<Transform>().position = respawnPosition.position;
			GetComponent<Health>().resetHealth();
			if (NetworkManager.isNetworkGame) GetComponent<NetworkView>().RPC("resetHealth", RPCMode.OthersBuffered, null);
			kill ();
			if (NetworkManager.isNetworkGame) GetComponent<NetworkView>().RPC("kill", RPCMode.OthersBuffered, null);
		}
        
		if (!(Network.isServer || Network.isClient) || GetComponent<NetworkView> ().isMine) {
			if(stunTimer != 0){
				stunTimer--;
				return;
			}
			if (Input.GetAxisRaw ("Horizontal" + playerNumber) != 0 || Input.GetAxisRaw ("Vertical" + playerNumber) != 0) {
				facing = new Vector2 (Input.GetAxisRaw ("Horizontal" + playerNumber), -Input.GetAxisRaw ("Vertical" + playerNumber)).normalized;
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
				animator.SetFloat ("Speed", Mathf.Abs (ha));
				if (NetworkManager.isNetworkGame) GetComponent<NetworkView>().RPC("setPlayerSpeed", RPCMode.OthersBuffered, Mathf.Abs (ha));
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
            if(slowTimer != 0)
            {
                model.GetComponent<SkinnedMeshRenderer>().material.color = Color.blue;
                slowTimer--;
            }
            else
            {
				if (NetworkManager.isNetworkGame) GetComponent<NetworkView>().RPC("playerIsFine", RPCMode.OthersBuffered);
                model.GetComponent<SkinnedMeshRenderer>().material.color = Color.white;
                moveSpeed = originalMoveSpeed;
            }

		}
	}

	void OnCollisionEnter2D (Collision2D hit) {
		if (hit.gameObject.tag == "Ground") {
			jumped = jumpTimes;
		} else if (hit.gameObject.tag == "Exp") {
			if (!(Network.isServer && Network.isClient) || GetComponent<NetworkView> ().isMine) {
				exp += 10;
				if (exp >= max_exp) {
					level++;
					abilityPoints++;
					exp = 0;
					levelUpText.SetActive(true);
					levelUpText.GetComponent<LevelUpText>().timer = 60;
				}
			}
			DestroyObject(hit.gameObject);
		} else {
			Physics2D.IgnoreCollision(hit.collider, GetComponent<Collider2D>());
		}
	}

	[RPC]
	void kill() {
		if (!GetComponent<NetworkView>().isMine)
			transform.GetChild (0).gameObject.SetActive (false);
		else
			gameObject.SetActive (false);
	}
	
	[RPC]
	void respawn() {
		if (!GetComponent<NetworkView>().isMine)
			transform.GetChild (0).gameObject.SetActive (true);
		else
			gameObject.SetActive (true);
	}

	[RPC]
	void playerIsSlowed() {
		moveSpeed = 0.8f;
		slowTimer = 60;
		model.GetComponent<SkinnedMeshRenderer>().material.color = Color.blue;
	}

	[RPC]
	void playerIsFine() {
		model.GetComponent<SkinnedMeshRenderer>().material.color = Color.white;
		moveSpeed = originalMoveSpeed;
	}

	[RPC]
	void setPlayerSpeed(float speed) {
		animator.SetFloat ("Speed", speed);
	}

	[RPC]
	void setPlayerAttack() {
		GetComponentInChildren<Animator> ().SetTrigger ("Attack");
	}

	[RPC]
	void setPlayerStun() {
		collider.GetComponent<Player>().stunTimer = 100;
	}
}
