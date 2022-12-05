using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSlotScript : MonoBehaviour
{
    public TMP_Text upgradeDescription;
    public Image upgradeImage;

    public LevelingUpgrades slotUpgrade;
    public UpgradeVersions slotVersion;
    public Rarity slotRarity;

    public LevelUpUIControllerScript controller;

    public void UpdateSlot(LevelingUpgrades upgrade, Rarity rarity)
    {
        slotUpgrade = upgrade;
        slotRarity = rarity;

        slotVersion = GetUpgradeVersion();

        upgradeImage.sprite = upgrade.UIImage;
        upgradeDescription.text = slotVersion.upgradeDescription;

    }
    private UpgradeVersions GetUpgradeVersion()
    {
        switch(slotRarity)
        {
            case Rarity.Common:
                return slotUpgrade.Common;
            case Rarity.Uncommon:
                return slotUpgrade.Uncommon;
            case Rarity.Rare:
                return slotUpgrade.Rare;
        }

        return slotUpgrade.Common; //deafults to common
    }

    public void OnUpgradePressed()
    {
        controller.OnSlotButtonPressed(slotUpgrade, slotRarity);
    }
}
