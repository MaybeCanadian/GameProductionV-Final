using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSlotScript : MonoBehaviour
{
    public TMP_Text slotAmountText;
    public Image slotImage;

    public GameObject slotParent;

    public ResourceItem slotResource;
    public int slotAmount;

    public void SetSlotVisible(bool input)
    {
        slotParent.SetActive(input);   
    }

    public void UpdateSlot(Materials material)
    {
        slotAmount = material.amount;
        slotAmountText.text = slotAmount.ToString();
        if (PlayerInventory.Instance.CheckIfHasItem(material.item, material.amount))
        {
            slotAmountText.color = Color.white;
        }
        else
        {
            slotAmountText.color = Color.red;
        }

        slotImage.sprite = material.item.UIImage;
    }
}
