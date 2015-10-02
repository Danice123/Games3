using UnityEngine;
using System.Collections;

public class frostBall : MonoBehaviour {

    public int damage = 10;
    public int ticksAlive = 30;
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
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground") || collider.CompareTag("shield"))
        {
            DestroyObject(gameObject);
        }
        if ((collider.CompareTag("Left") || collider.CompareTag("Right")) && !collider.CompareTag(owner))
        {
            collider.gameObject.GetComponent<Health>().health -= damage;
            DestroyObject(gameObject);
        }
    }
}
