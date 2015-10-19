using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cooldownView : MonoBehaviour {
    public GameObject x, square, circle, triangle;
    public GameObject player;
    public GameObject HUD;
    public GameObject healthBar;
    public GameObject expBar;
    public GameObject circleOutline, squareOutline, triangleOutline, xOutline;
    // Use this for initialization
    private float healthBarOriginalPos, expBarOriginalPos;
    private int previousHealth, previousExp;
    public float startingHealthPos;
    void Start () {
        healthBarOriginalPos = healthBar.transform.position.x;
        expBarOriginalPos = expBar.transform.position.x;
        previousHealth = player.GetComponent<Health>().health;
        startingHealthPos = healthBar.transform.position.x;
    }
    public void initPlayer()
    {
        healthBarOriginalPos = healthBar.transform.position.x;
        expBarOriginalPos = expBar.transform.position.x;
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

		var sImage = square.GetComponent<Image>();
		c = sImage.color;
		if(player.GetComponent<Player>().squareCooldownTimer != 0)
		{
			c.a = (1.0f - (player.GetComponent<Player>().squareCooldownTimer + 0.0f) / player.GetComponent<Player>().squareCooldown)/2.0f;
		}
		else
		{
			c.a = 1.0f;
		}
		sImage.color = c;


        if (previousHealth != player.GetComponent<Health>().health && previousHealth != 0)
        {
                //healthBar.transform.Translate(new Vector3(healthBarOriginalPos - healthBar.transform.position.x, 0, 0));
                healthBar.transform.Translate(new Vector3(((float)player.GetComponent<Health>().health - previousHealth) * ((RectTransform)healthBar.transform).GetComponent<RectTransform>().rect.width 
                    / (float)player.GetComponent<Health>().maxHealth, 0, 0));

        }
        if (previousExp != player.GetComponent<Player>().exp)
        {
            expBar.transform.Translate(new Vector3(((float)player.GetComponent<Player>().exp - previousExp) * ((RectTransform)expBar.transform).GetComponent<RectTransform>().rect.width
                    / (float)player.GetComponent<Player>().max_exp, 0, 0));

        }
		if (player.GetComponent<Player>().exp >= player.GetComponent<Player>().max_exp)
		{
			expBar.transform.position.Set(expBarOriginalPos, expBar.transform.position.y, expBar.transform.position.z);
		}
		if(player.GetComponent<Health>().health <= 0)
		{
			healthBar.transform.position.Set(healthBarOriginalPos, healthBar.transform.position.y, healthBar.transform.position.z);
		}
        previousExp = player.GetComponent<Player>().exp;
        previousHealth = player.GetComponent<Health>().health;
        squareOutline.GetComponent<Image>().fillAmount = (float)player.GetComponent<Player>().ability1Level / 4.0f;
        triangleOutline.GetComponent<Image>().fillAmount = (float)player.GetComponent<Player>().ability2Level / 4.0f;
        circleOutline.GetComponent<Image>().fillAmount = (float)player.GetComponent<Player>().ability3Level / 4.0f;
        //circleOutline.GetComponent<Image>().fillAmount = (float)player.GetComponent<Player>().ability1Level / 4.0f;
    }

}
