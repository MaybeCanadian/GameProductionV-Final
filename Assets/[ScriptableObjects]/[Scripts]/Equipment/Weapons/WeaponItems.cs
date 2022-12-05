using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItems : ScriptableObject
{
    [Header("Weapon Information")]
    [Tooltip("The name of the weapon in menus")]
    public string weaponName = "";
    [Tooltip("The type of weapon this is")]
    public WeaponTypes weaponType;
    [Tooltip("The physical game object the player holds")]
    public GameObject weaponPrefab;
    [Tooltip("The image shown in UI's for the weapon")]
    public Sprite UIimage;
    [Tooltip("The base damage of the weapon")]
    public Vector2 attackDamage = new Vector2(80.0f, 120.0f);
    [Tooltip("What types of things can the weapon hit?")]
    public LayerMask enemyMask;

    public virtual void attack(Transform currentPosition) { }
}
