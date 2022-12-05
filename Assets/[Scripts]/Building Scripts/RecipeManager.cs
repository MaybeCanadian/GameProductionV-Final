using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;

    public List<BuildingRecipes> recipes = new List<BuildingRecipes>(); 

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public List<BuildingRecipes> GetRecipes()
    {
        return recipes;
    }

}
