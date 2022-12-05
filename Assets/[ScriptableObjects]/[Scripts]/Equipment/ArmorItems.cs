using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Equipment/Armor")]
public class ArmorItems : ScriptableObject
{
    [Tooltip("Name shown on menus")]
    public string armorName = "";
    [Tooltip("Armor Piece shown on the body of the player")]
    public GameObject armorPrefab;
    [Tooltip("How good the armor is")]
    public float armorValue;
    [Tooltip("Sprite used when seen in UI")]
    public Sprite armorSprite;

    //we can also add some extra effects here
}
