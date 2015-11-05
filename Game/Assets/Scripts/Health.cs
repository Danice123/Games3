using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int health;
	public int maxHealth;
	public int xOffset = 40;
	public int yOffset = 50;
	public bool showBar = true;

	public GameObject model;

	private Texture2D healthFront;
	private Texture2D healthBack;
	private GUIStyle RectStyle;

	// Use this for initialization
	void Start () {
		healthFront = new Texture2D (1, 1);
		healthBack = new Texture2D (1, 1);
		RectStyle = new GUIStyle ();
		healthFront.SetPixel (0, 0, Color.green);
		healthFront.Apply ();
		healthBack.SetPixel (0, 0, Color.red);
		healthBack.Apply ();
	}
	
	void OnGUI() {
		if (Camera.current == null || !showBar)
			return;
		Vector3 screenPosition = Camera.current.WorldToScreenPoint(transform.position);
		
		screenPosition.y = Screen.height - (screenPosition.y + 1);
		
		Rect rect = new Rect(screenPosition.x - xOffset, screenPosition.y - yOffset, 80, 5);
		RectStyle.normal.background = healthBack;
		GUI.Box (rect, GUIContent.none, RectStyle);
		
		rect = new Rect(screenPosition.x - xOffset, screenPosition.y - yOffset, 80 * ((float) health / (float) maxHealth), 5);
		RectStyle.normal.background = healthFront;
		GUI.Box (rect, GUIContent.none, RectStyle);
	}

	void Update () {
		if (model != null) {
			Color o = model.GetComponent<SkinnedMeshRenderer> ().material.color;
			Color n = new Color (o.r, health / (float)maxHealth, health / (float)maxHealth);
			model.GetComponent<SkinnedMeshRenderer> ().material.color = n;
		}
	}

	[RPC]
	public void changeHealth(int change) {
		health += change;
	}

	[RPC]
	public void changeMaxHealth(int change) {
		maxHealth += change;
	}

	[RPC]
	public void resetHealth() {
		health = maxHealth;
	}
}
