using UnityEngine;
using System.Collections;

public class frostBall : MonoBehaviour {

    public int damage = 10;
    public int ticksAlive = 30;
    public int rotateSpeed = 0;
    public string owner;
    // Use this for initialization
    void Start() {

    }

    void FixedUpdate()
    {
		if (ticksAlive == 0) {
			gameObject.SetActive(false);
		}
        ticksAlive--;

        Vector2 dir = GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
      
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground") || collider.CompareTag("shield"))
        {
			gameObject.SetActive(false);
        }
        if (!collider.CompareTag(owner))
        {
            UnityEngine.Debug.Log("Inner If");
            if (collider.GetComponent<Health>() == null) return;
			if (!Network.isClient) {
	            collider.gameObject.GetComponent<Health>().health -= damage;
				if(collider.GetComponent<Player>() != null){
					//slow to fix still
					collider.GetComponent<Player>().moveSpeed = 0.8f;
	                collider.GetComponent<Player>().slowTimer = 60;
	                collider.GetComponent<Player>().model.GetComponent<SkinnedMeshRenderer>().material.color = Color.blue;
				}
			}
			gameObject.SetActive(false);
        }
    }
}
