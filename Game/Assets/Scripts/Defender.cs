using UnityEngine;
using System.Collections;

public class Defender : MonoBehaviour {
	public GameObject turret;
	public GameObject fire;
	int fireCounter = 0;

	void FixedUpdate() {
		if (!Network.isClient && GetComponent<Health> ().health <= 0){
			if (Network.isServer)
				GameObject.Find("NetworkManager").GetComponent<NetworkView> ().RPC("endGame", RPCMode.AllBuffered, tag);
			else
				GameObject.Find("GameManager").GetComponent<GameManager>().endGame(tag);

			if (NetworkManager.isNetworkGame) turret.GetComponent<NetworkView> ().RPC("kill", RPCMode.OthersBuffered, null);
			turret.GetComponent<Tower>().kill();
		}

			int health = GetComponent<Health>().health;
			if (health <= 900 && fireCounter == 0)
			{
				var fireObject = (GameObject)Instantiate(fire, turret.transform.position + new Vector3(0, 1, -1), Quaternion.identity);
				fireObject.transform.parent = turret.transform;
				fireCounter++;
			}
			if(health <= 800 && fireCounter == 1)
			{
			var fireObject = (GameObject)Instantiate(fire, turret.transform.position + new Vector3(1.5f, 1, -1), Quaternion.identity);
				fireObject.transform.parent = turret.transform;
				
				fireCounter++;
			}
			if(health <= 700 && fireCounter == 2)

			{
				var fireObject = (GameObject)Instantiate(fire, turret.transform.position + new Vector3(-1.5f, 1, -1), Quaternion.identity);
				fireObject.transform.parent = turret.transform;
				
				fireCounter++;
			}
			if(health <= 600 && fireCounter == 3)
			{
				var fireObject = (GameObject)Instantiate(fire, turret.transform.position + new Vector3(0, 2, -1), Quaternion.identity);
				fireObject.transform.parent = turret.transform;

				fireCounter++;
			}
			if(health <= 500 && fireCounter == 4)
			{
				var fireObject = (GameObject)Instantiate(fire, turret.transform.position + new Vector3(1, 2, -1), Quaternion.identity);
				fireObject.transform.parent = turret.transform;

				fireCounter++;
			}
			if (health <= 400 && fireCounter == 5)
			{
				var fireObject = (GameObject)Instantiate(fire, turret.transform.position + new Vector3(-1, 2, -1), Quaternion.identity);
				fireObject.transform.parent = turret.transform;
				fireCounter++;
			}
			if(health <= 300 && fireCounter == 6)
			{
			var fireObject = (GameObject)Instantiate(fire, turret.transform.position + new Vector3(0.5f, 3, -1), Quaternion.identity);
				fireObject.transform.parent = turret.transform;
				
				fireCounter++;
			}
			if(health <= 200 && fireCounter == 7)

			{
				var fireObject = (GameObject)Instantiate(fire, turret.transform.position + new Vector3(-0.5f, 3, -1), Quaternion.identity);
				fireObject.transform.parent = turret.transform;
				
				fireCounter++;
			}
			if(health <= 100 && fireCounter == 8)
			{
				var fireObject = (GameObject)Instantiate(fire, turret.transform.position + new Vector3(0, 4, -1), Quaternion.identity);
				fireObject.transform.parent = turret.transform;

				fireCounter++;
			}
			

	}
}
