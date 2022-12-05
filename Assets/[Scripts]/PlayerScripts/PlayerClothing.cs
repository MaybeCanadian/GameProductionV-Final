using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothing : MonoBehaviour
{
    [Header("Armor Sets")]
    public List<clothingSet> armorSets;

    private void Start()
    {
        if(armorSets.Count > 0)
        {
            clothingSet armor = armorSets[Random.Range(0, armorSets.Count)];

            loadArmor(armor);
        }
    }

    private void loadArmor(clothingSet armor)
    {
        if(armor.headSlot)
        {
            armor.headSlot.SetActive(true);
        }
        if (armor.chestSlot)
        {
            armor.chestSlot.SetActive(true);
        }
        if (armor.legSlot)
        {
            armor.legSlot.SetActive(true);
        }
        if (armor.gloveSlot)
        {
            armor.gloveSlot.SetActive(true);
        }
        if (armor.shoulderSlot)
        {
            armor.shoulderSlot.SetActive(true);
        }
        if (armor.feetSlot)
        {
            armor.feetSlot.SetActive(true);
        }

    }
}
