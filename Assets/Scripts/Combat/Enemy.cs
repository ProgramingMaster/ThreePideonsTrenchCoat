using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 120;
    public WeaponStats weaponStats;
    public Transform firePoint;
    public GameObject bulletPrefab;
    string origin = "Enemy";

    void Start() {
        InvokeRepeating("Shoot", 1.0f, 1.0f);
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().RecieveBulletParameter(weaponStats.damage, weaponStats.firerate, weaponStats.range, origin);
    }
}
