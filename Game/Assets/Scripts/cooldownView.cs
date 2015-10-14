using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cooldownView : MonoBehaviour {
    public GameObject x, square, circle, triangle;
    public GameObject player;
    public GameObject HUD;
    public GameObject healthBar;
    // Use this for initialization

    private Texture2D healthFront;
    private Texture2D healthBack;
    private GUIStyle RectStyle;
    private float healthBarOriginalPos;
    private int previousHealth;
    public float startingHealthPos;
    void Start () {
        healthFront = new Texture2D(1, 1);
        healthBack = new Texture2D(1, 1);
        RectStyle = new GUIStyle();
        healthFront.SetPixel(0, 0, Color.green);
        healthFront.Apply();
        healthBack.SetPixel(0, 0, Color.red);
        healthBack.Apply();
        healthBarOriginalPos = healthBar.transform.position.x;
        previousHealth = player.GetComponent<Health>().health;
        startingHealthPos = healthBar.transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
            var tImage = triangle.GetComponent<Image>();
            Color c = tImage.color;
            if(player.GetComponent<Player>().triangleCooldownTimer != 0)
            {
                c.a = (1.0f - (player.GetComponent<Player>().triangleCooldownTimer + 0.0f) / player.GetComponent<Player>().triangleCooldown)/2.0f;
            }
            else
            {
                c.a = 1.0f;
            }
            tImage.color = c;
        //healthBar.transform.Translate(new Vector3(-0.5f, 0, 0));
        //(RectTransform)healthBar.transform).GetComponent<RectTransform>().rect.width *

        if (previousHealth != player.GetComponent<Health>().health && previousHealth != 0)
            {
                //healthBar.transform.Translate(new Vector3(healthBarOriginalPos - healthBar.transform.position.x, 0, 0));
                healthBar.transform.Translate(new Vector3(((float)player.GetComponent<Health>().health - previousHealth) * ((RectTransform)healthBar.transform).GetComponent<RectTransform>().rect.width 
                    / (float)player.GetComponent<Health>().maxHealth, 0, 0));
                if(player.GetComponent<Health>().health <= 0)
                {
                    healthBar.transform.Translate(new Vector3(healthBarOriginalPos - healthBar.transform.position.x, 0, 0));
                }
            }

        previousHealth = player.GetComponent<Health>().health;

    }

}
