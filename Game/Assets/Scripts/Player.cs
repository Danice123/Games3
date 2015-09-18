using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public int jumpTimes = 2;
	public float moveSpeed = 1.0f;
	public float jumpSpeed = 1.0f;

	public GameObject arrow;
	public int arrowSpeed = 10;
    public GameObject samurai;

	public GameObject largeArrow;
	public int largeArrowSpeed = 20;

	private Vector2 facing = new Vector2(1, 0);
    private Vector2 prevFacing = new Vector2(1, 0);
    // Use this for initialization
    void Start () {
        samurai = GameObject.Find("samurai");
        samurai.GetComponent<Animation>()["Attack"].layer = 3;
        samurai.GetComponent<Animation>()["Jump"].layer = 2;
        samurai.GetComponent<Animation>()["Attack"].wrapMode = WrapMode.Once;
        samurai.GetComponent<Animation>()["Jump"].wrapMode = WrapMode.Once;
        samurai.GetComponent<Animation>()["Attack"].speed = 2.0f;
        samurai.GetComponent<Animation>()["Run"].layer = 1;
        samurai.GetComponent<Animation>()["Run"].wrapMode = WrapMode.Once;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
        bool buttonPressed = false;
		if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) {
			facing = new Vector2(Input.GetAxisRaw("Horizontal"), -Input.GetAxisRaw("Vertical")).normalized;
            
            samurai.GetComponent<Animation>().Play("Run");

            if (facing.x > 0.0f && prevFacing.x < 0.0f || facing.x < 0.0f && prevFacing.x > 0.0f)
            {
                samurai.transform.Rotate(0, 180, 0);
                
            }
            prevFacing = facing;
           
        }

		Vector2 vel = GetComponent<Rigidbody2D> ().velocity;

		float ha = Input.GetAxisRaw ("Horizontal") * moveSpeed;

		if (Input.GetButtonDown ("A") && jumpTimes > 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, jumpSpeed);
			jumpTimes--;
            samurai.GetComponent<Animation>().Play("Jump");
            buttonPressed = true;
        } else {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, vel.y);
		}

		if (Input.GetButtonDown ("X")) {
			GameObject a = (GameObject) Instantiate(arrow, GetComponent<Transform>().position, Quaternion.identity);
			a.GetComponent<Rigidbody2D>().velocity = facing * arrowSpeed + new Vector2(0, 5);
			a.tag = gameObject.tag;
            samurai.GetComponent<Animation>().Play("Attack");
            buttonPressed = true;
        }

		if (Input.GetButtonDown ("Y")) {
			GameObject a = (GameObject) Instantiate(largeArrow, GetComponent<Transform>().position, Quaternion.identity);
			a.GetComponent<Rigidbody2D>().velocity = facing * largeArrowSpeed;
			a.tag = gameObject.tag;
            buttonPressed = true;
        }
     
            samurai.GetComponent<Animation>().Play("idle");
        
	}

	void OnCollisionEnter2D (Collision2D hit) {
		if (hit.gameObject.tag == "Ground") {
			jumpTimes = 2;
		}
	}
}
