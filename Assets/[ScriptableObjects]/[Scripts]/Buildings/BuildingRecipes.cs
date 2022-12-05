using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Recipe", menuName = "Buildings/Recipes")]
public class BuildingRecipes : ScriptableObject
{
    [Tooltip("Name shown in menus")]
    public string recipeName = "";
    [Tooltip("The building the recipe makes")]
    public BuildingItems building;
    [Tooltip("The image shown in the UI")]
    public Sprite recipeImage;
    [Tooltip("The materials required to make the building")]
    public List<Materials> requiredMaterials;
}

[System.Serializable]
public struct Materials
{
    [Tooltip("Item object required")]
    public ResourceItem item;
    [Tooltip("Number of the objects needed to craft")]
    public int amount;
}
