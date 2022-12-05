using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour
{
    [Header("Slot info")]
    [SerializeField]
    private string itemName = "";
    [SerializeField]
    private int itemCount = 0;
    [SerializeField]
    private Sprite itemSprite;

    [Header("Slot references")]
    [SerializeField]
    private GameObject slotParent;
    [SerializeField]
    private TMP_Text slotText;
    [SerializeField]
    private TMP_Text slotCount;
    [SerializeField]
    private Image slotImage;

    private void Awake()
    {
        UpdateSlot();
        SlotVisible(false);
    }

    public void SetItemSprite(Sprite input)
    {
        itemSprite = input;
    }

    public void SetItemCount(int input)
    {
        itemCount = input;
    }

    public void SetItemName(string input)
    {
        itemName = input;
    }

    public void UpdateSlot()
    {
        slotText.text = itemName;
        slotCount.text = itemCount.ToString();
        slotImage.sprite = itemSprite;
    }

    public void SlotVisible(bool input)
    {
        slotParent.SetActive(input);
    }
}
