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
            DestroyObject(gameObject);
        ticksAlive--;

        Vector2 dir = GetComponent<Rigidbody2D>().velocity;
       
      
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground") || collider.CompareTag("shield"))
        {
            DestroyObject(gameObject);
        }
        if (!collider.CompareTag(owner))
        {
            UnityEngine.Debug.Log("Inner If");
            if (collider.GetComponent<Health>() == null) return;
            collider.gameObject.GetComponent<Health>().health -= damage;
            DestroyObject(gameObject);
        }
    }
}
