using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Buildings/Building")]
public class BuildingItems : ScriptableObject
{
    [Tooltip("The name of the Building in the menus")]
    public string buildingName = "";
    [Tooltip("The building Prefab to be placed in game")]
    public GameObject building;
}
