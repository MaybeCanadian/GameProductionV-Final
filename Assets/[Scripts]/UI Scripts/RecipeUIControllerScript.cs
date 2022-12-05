using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUIControllerScript : MonoBehaviour
{
    public GameObject recipeUI;

    public List<RecipeSlotScript> recipeSlots;

    public void OnRecipeButtonPressed()
    {
        if (GameStateMachine.GetInstance().GetState() == GameStates.GAME) 
        { 
            if(!recipeUI.activeInHierarchy)
            {
                OpenUi();
            }
        }
        else if(GameStateMachine.GetInstance().GetState() == GameStates.RECIPE)
        {
            CloseUI();
        }
    }

    private void UpdateSlots()
    {
        List<BuildingRecipes> recipes = RecipeManager.instance.GetRecipes();

        int itt = 0;

        foreach(RecipeSlotScript slot in recipeSlots)
        {
            if(itt < recipes.Count)
            {
                slot.SetSlotVisible(true);
                slot.UpdateSlot(recipes[itt]);
            }
            else
            {
                slot.SetSlotVisible(false);
            }

            itt++;
        }
    }

    public void OnSlotButtonPressed(BuildingRecipes recipe) 
    {
        if (PlayerInventory.Instance.RemoveAllItemsInList(recipe.requiredMaterials))
        {
            CloseUI();
            BuildingManagerScript.instance.EnterBuildingMode(recipe);

            return;
        }
       

        //can do a noise or something here
    }

    private void OpenUi()
    {
        recipeUI.SetActive(true);
        GameStateMachine.GetInstance().ChangeState(GameStates.RECIPE);
        UpdateSlots();
    }

    private void CloseUI()
    {
        recipeUI.SetActive(false);
        GameStateMachine.GetInstance().ChangeState(GameStates.GAME);
    }
}
