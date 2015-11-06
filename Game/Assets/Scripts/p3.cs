using UnityEngine;
using System.Collections;

public class p3 : MonoBehaviour
{

    public int jumpTimes = 2;
    public float moveSpeed = 1.0f;
    public float jumpSpeed = 1.0f;
    public int respawnTimer = 60;
    public GameObject frostBall;
    public GameObject fireBall;
    public bool canMove = true;
    const float FROSTBALL_SPEED = 15.0f;
    const float FIREBALL_SPEED = 20.0f;
    public Vector2 facing = new Vector2(1, 0);
    public int jumped = 0;
    private Player player;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>();
        player.triangleCooldown = 240;
		player.squareCooldown = 15;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 facing = GetComponent<Player>().facing;
        if (facing.x > 0)
        {
            GetComponentsInChildren<Transform>()[1].rotation = Quaternion.AngleAxis(90, Vector3.up);
        }
        else
        {
            GetComponentsInChildren<Transform>()[1].rotation = Quaternion.AngleAxis(-90, Vector3.up);
        }
    }

    void FixedUpdate()
    {
        Vector2 facing = player.facing;
        string playerNumber = GetComponent<Player>().playerNumber;
        if (GetComponent<Health>().health <= 0)
        {
            respawnTimer = 60 * 1;
            GetComponent<Transform>().position = new Vector2(0, 2);
            GetComponent<Health>().health = GetComponent<Health>().maxHealth;
            gameObject.SetActive(false);
        }
        if (Input.GetButtonDown(playerNumber + "Ability1") && player.squareCooldownTimer == 0)
        {
			for(int b = 0; b <= player.ability1Level; b++){
				float angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
				GameObject a = (GameObject)Instantiate(frostBall, GetComponent<Transform>().position + new Vector3(0, 1, 0), Quaternion.AngleAxis(angle, Vector3.forward));
				a.GetComponent<Rigidbody2D>().velocity = facing * FROSTBALL_SPEED + new Vector2(0, 5);
				a.GetComponent<frostBall>().owner = gameObject.tag;
				GetComponentInChildren<Animator>().SetTrigger("Attack");
				player.squareCooldownTimer = 15;
			}
		}
		if (Input.GetButtonDown(playerNumber + "Ability2") && player.triangleCooldownTimer == 0)
        {
            float angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
            for (int b = 0; b< 3 + 2 * player.ability2Level; b++)
            {
                GameObject a = (GameObject)Instantiate(fireBall, GetComponent<Transform>().position + new Vector3(0, 1, 0), Quaternion.AngleAxis(angle, Vector3.forward));
                a.GetComponent<Rigidbody2D>().velocity = facing * FIREBALL_SPEED;
                a.GetComponent<fireball>().owner = gameObject.tag;
            }
            GetComponentInChildren<Animator>().SetTrigger("Attack");
            player.triangleCooldownTimer = 240;
        }
        else
        {
            player.triangleCooldownTimer--;
            if(player.triangleCooldownTimer < 0)
            {
                player.triangleCooldownTimer = 0;
            }
			player.squareCooldownTimer--;
			if(player.squareCooldownTimer < 0)
			{
				player.squareCooldownTimer = 0;
			}
        }
        //if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        //{
        //    facing = new Vector2(Input.GetAxisRaw("Horizontal"), -Input.GetAxisRaw("Vertical")).normalized;
        //}
        Vector2 vel = GetComponent<Rigidbody2D>().velocity;
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "Ground")
        {
            jumped = jumpTimes;
        }
    }
}
