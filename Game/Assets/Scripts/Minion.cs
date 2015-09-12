using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {

	public int health = 100;
	public int maxHealth = 100;

	public bool isLeftMinion = true;
	public float moveSpeed = 1.0f;

	private Texture2D Health;
	private Texture2D HealthBack;
	private GUIStyle RectStyle;

	private bool attackMode = false;
	public int attackCooldown = 20;

	// Use this for initialization
	void Start () {
		Health = new Texture2D (1, 1);
		HealthBack = new Texture2D (1, 1);
		RectStyle = new GUIStyle ();
		Health.SetPixel (0, 0, Color.green);
		Health.Apply ();
		HealthBack.SetPixel (0, 0, Color.red);
		HealthBack.Apply ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		Vector3 screenPosition = Camera.current.WorldToScreenPoint(transform.position);

		screenPosition.y = Screen.height - (screenPosition.y + 1);

		Rect rect = new Rect(screenPosition.x - 40, screenPosition.y - 50, 80, 5);
		RectStyle.normal.background = HealthBack;
		GUI.Box (rect, GUIContent.none, RectStyle);

		rect = new Rect(screenPosition.x - 40, screenPosition.y - 50, 80 * ((float) health / (float) maxHealth), 5);
		RectStyle.normal.background = Health;
		GUI.Box (rect, GUIContent.none, RectStyle);
	}

	void FixedUpdate() {
		if (health <= 0)
			DestroyObject (this.gameObject);
		Vector2 vel = GetComponent<Rigidbody2D> ().velocity;

		if (attackMode) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2(0, 0);

			if (attackCooldown <= 0) {
				if (target == null) {
					attackMode = false;
					return;
				}
				target.GetComponent<Minion> ().health -= 10;
				attackCooldown = 20;
			}
			if (attackCooldown > 0) attackCooldown--;
		} else {
			if (isLeftMinion)
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, vel.y);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, vel.y);
		}
	}

	private GameObject target;

	void OnTriggerEnter2D ( Collider2D collider) {
		if (!attackMode) {
			if (collider.CompareTag ("Left") || collider.CompareTag ("Right")) {
				attackMode = true;
				target = collider.gameObject;
			}
		}
	}

	void OnTriggerExit2D ( Collider2D collider) {
		if (attackMode) {
			if (collider.CompareTag ("Left") || collider.CompareTag ("Right")) {
				attackMode = false;
			}
		}
	}
}
