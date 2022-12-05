using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Leveling Upgrade", menuName = "Upgrades/Leveling")]
public class LevelingUpgrades : ScriptableObject
{
    [Header("Shared Values")]
    public string groupName;
    [Tooltip("The stat to be change")]
    public StatTypes upgradeType;
    [Tooltip("The way to change the stat")]
    public StatChangeMethod upgradeMethod;
    [Tooltip("The image shown in the menus for the upgrade")]
    public Sprite UIImage = null;

    [Header("Common")]
    public UpgradeVersions Common;

    [Header("Uncommong")]
    public UpgradeVersions Uncommon;
    [Tooltip("The chance the upgrade is uncommon, uncommon is checked seccond"), Range(0.0f, 100.0f)]
    public float UncommonChance;

    [Header("Rare")]
    public UpgradeVersions Rare;
    [Tooltip("The chance the upgrade is Rare. Rare is checked first"), Range(0.0f, 100.0f)]
    public float RareChance;

    public void PickUpgrade(Rarity version) 
    {
        float amount = 0;

         switch(version)
        {
            case Rarity.Common:
                amount = Common.amount;
                break;
            case Rarity.Uncommon:
                amount = Uncommon.amount;
                break;
            case Rarity.Rare:
                amount = Rare.amount;
                break;
        }

        PlayerStatsScript.instance.ModifyStat(upgradeType, amount, upgradeMethod);
    }
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare
}

[System.Serializable]
public struct UpgradeVersions
{
    [TextArea, Tooltip("the In Game description of the effect")]
    public string upgradeDescription;
    [Tooltip("The bonuses the upgrade gives")]
    public float amount;
}
