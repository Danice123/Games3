using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuScript : MonoBehaviour {
    int test = 0;
    int numSelected;
    int charsChosen = 0;
    public GameObject chooseLabel;
    public GameObject persistObject;
    Text labelText;
	// Use this for initialization
	void Start () {
        persistObject = GameObject.Find("persistObject");
        labelText = chooseLabel.GetComponent<Text>();
	}
	public void LoadCharSelect(int gameType)
    {
        persistObject.GetComponent<menuObjectScript>().gameType = gameType;
        if(persistObject.GetComponent<menuObjectScript>().gameType >= 3)
        {

        }
        DontDestroyOnLoad(persistObject);
        Application.LoadLevel(1);
    }
    
	public void LoadGame(int ch) {
        //Multiplayer Game mode
        if (persistObject.GetComponent<menuObjectScript>().gameType > 2)
        {
            if(charsChosen == 1)
            {
                persistObject.GetComponent<menuObjectScript>().player2 = ch;
                Application.LoadLevel(persistObject.GetComponent<menuObjectScript>().gameType);
            }
            else
            {
                persistObject.GetComponent<menuObjectScript>().player1 = ch;
                charsChosen = 1;
                //labelText = "Player Two Choose" as Text;
            }
        }
        else
        {
            PlayerSpawn.playerType = ch;
            Application.LoadLevel(persistObject.GetComponent<menuObjectScript>().gameType);
        }

	}
	// Update is called once per frame
	void Update () {
		
	}
}
