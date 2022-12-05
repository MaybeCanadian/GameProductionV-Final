using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlotScript : MonoBehaviour
{
    public TMP_Text slotName;
    public Image recipeImage;
    public GameObject slotParent;

    public BuildingRecipes slotRecipe;

    public List<MaterialSlotScript> materialSlots;

    public RecipeUIControllerScript controller;

    public void OnSlotPressed()
    {
        controller.OnSlotButtonPressed(slotRecipe);
    }

    public void SetSlotVisible(bool input)
    {
        slotParent.SetActive(input);
    }

    public void UpdateSlot(BuildingRecipes recipe) 
    {
        slotRecipe = recipe;

        slotName.text = slotRecipe.name;
        recipeImage.sprite = slotRecipe.recipeImage;

        int itt = 0;
        foreach(MaterialSlotScript slot in materialSlots)
        {
            if(itt < slotRecipe.requiredMaterials.Count)
            {
                slot.SetSlotVisible(true);
                slot.UpdateSlot(slotRecipe.requiredMaterials[itt]);
            }
            else
            {
                slot.SetSlotVisible(false);
            }

            itt++;
        }
    }
}
