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
        if (ticksAlive == 0)
            DestroyObject(gameObject);
        ticksAlive--;

        Vector2 dir = GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
      
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
