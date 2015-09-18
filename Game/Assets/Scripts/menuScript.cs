using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class menuScript : MonoBehaviour {

    public GameObject Char1;
    public GameObject Char2;
    public GameObject Char3;
    
	// Use this for initialization
	void Start () {
        
        Char1 = GameObject.Find("Char 1");
        
        //Char1.GetComponent<Button>()
	}
	public void LoadScene(int level)
    {
        Application.LoadLevel(level);
    }

	public void LoadPractice(int ch) {
		PlayerSpawn.playerType = ch;
		Application.LoadLevel(3);

	}
	// Update is called once per frame
	void Update () {
        var tilt = new Vector2(Input.GetAxisRaw("Horizontal"), -Input.GetAxisRaw("Vertical")).normalized;

    }
}
