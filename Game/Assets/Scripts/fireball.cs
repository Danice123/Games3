using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {

    public int damage = 30;
    public int ticksAlive = 50;
    public int rotateSpeed = 100;
    public string owner;
    // Use this for initialization
    void Start() {

    }

    void FixedUpdate()
    {
        if (ticksAlive == 0)
			gameObject.SetActive(false);
        ticksAlive--;

        Vector2 dir = GetComponent<Rigidbody2D>().velocity;
       
      
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
			if (!Network.isClient) collider.gameObject.GetComponent<Health>().health -= damage;
			gameObject.SetActive(false);
        }
    }
}
