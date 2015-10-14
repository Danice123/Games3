using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cooldownView : MonoBehaviour {
    public GameObject x, square, circle, triangle;
    public GameObject player;
	// Use this for initialization
	void Start () {
	    
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
        //Rect rect = new Rect(screenPosition.x - xOffset, screenPosition.y - yOffset, 80, 5);
        //RectStyle.normal.background = healthBack;
        //GUI.Box(rect, GUIContent.none, RectStyle);

        //rect = new Rect(screenPosition.x - xOffset, screenPosition.y - yOffset, 80 * ((float)health / (float)maxHealth), 5);
        //RectStyle.normal.background = healthFront;
        //GUI.Box(rect, GUIContent.none, RectStyle);

        tImage.color = c;
        ;
	}
}
