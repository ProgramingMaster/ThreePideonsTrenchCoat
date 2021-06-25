using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    float damage;
    float firerate;
    float range;
    float x;
    string origin;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        x = rb.position.x;
    }

    public void RecieveBulletParameter (float _damage, float firerate, float _range, string _origin) {
        damage = _damage;
        range = _range;
        origin = _origin;
    }

    void Update() {
        if (rb.position.x > x+range || rb.position.x < x-range) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D (Collider2D hitinfo) {
        if (hitinfo.tag == "Wall") {
            Destroy(gameObject);
        }
        if (origin == "Enemy") {
            Player player = hitinfo.GetComponent<Player>();
            if (player != null) {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        if (origin == "Player") {
            Enemy enemy = hitinfo.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
