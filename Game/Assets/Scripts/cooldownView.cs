using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cooldownView : MonoBehaviour {
    public GameObject x, square, circle, triangle;
    public GameObject player;
    public GameObject HUD;
	public GameObject levelUp;
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
		Color temp = levelUp.GetComponent<Image> ().color;
		temp.a = 0;
		levelUp.GetComponent<Image> ().color = temp;
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
		if (player == null)
			return;
        var tImage = triangle.GetComponent<Image>();
		var levelupImage = levelUp.GetComponent<Image>();
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
	

		//Level up indicator
		if (player.GetComponent<Player> ().abilityPoints > 0) {
			Color temp = levelupImage.color;
			temp.a = 1;
			levelUp.GetComponent<Image> ().color = temp;
		} else {
			Color temp = levelupImage.color;
			temp.a = 0;
			levelUp.GetComponent<Image> ().color = temp;
		}



		Vector3 healthTemp = new Vector3 (healthBarOriginalPos - (float)healthBar.GetComponent<Image>().rectTransform.rect.width *
		                      (1 - (float)player.GetComponent<Health> ().health / (float)player.GetComponent<Health> ().maxHealth),
		                      healthBar.transform.position.y,
		                      healthBar.transform.position.z);
		Vector3 expTemp = new Vector3 ( expBarOriginalPos + (float)expBar.GetComponent<Image>().rectTransform.rect.width *
		                                  (float)player.GetComponent<Player> ().exp / (float)player.GetComponent<Player> ().max_exp,
		                                  expBar.transform.position.y,
		                                  expBar.transform.position.z);
		healthBar.transform.position = healthTemp;
		expBar.transform.position = expTemp;
		
        squareOutline.GetComponent<Image>().fillAmount = (float)player.GetComponent<Player>().ability1Level / 4.0f;
        triangleOutline.GetComponent<Image>().fillAmount = (float)player.GetComponent<Player>().ability2Level / 4.0f;
        circleOutline.GetComponent<Image>().fillAmount = (float)player.GetComponent<Player>().ability3Level / 4.0f;
        //circleOutline.GetComponent<Image>().fillAmount = (float)player.GetComponent<Player>().ability1Level / 4.0f;
    }

}
