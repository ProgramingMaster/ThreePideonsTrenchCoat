using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon Stats", menuName = "WeaponStats")]
public class WeaponStats : ScriptableObject
{
    public float damage;
    public string firetype;
    public float firerate;
    public float range;
} 