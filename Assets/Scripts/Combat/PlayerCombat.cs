using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public WeaponStats weaponStats;
    string origin = "Player";
    public bool CombatStart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().RecieveBulletParameter(weaponStats.damage, weaponStats.firerate, weaponStats.range, origin);
    }
}
