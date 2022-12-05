using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeChosingScript : MonoBehaviour
{
    public static UpgradeChosingScript instance;

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

    public List<LevelingUpgrades> allUpgrades;

    public List<LevelingUpgrades> ChooseThreeUpgrades(out List<Rarity> upgradeRarity)
    {
        upgradeRarity = new List<Rarity>();

        if (!(allUpgrades.Count > 3))
        {
            return null;
        }
        List<LevelingUpgrades> chosenUpgrades = new List<LevelingUpgrades>();

        List<int> itterators = new List<int>();
        int itt = 0;
        foreach(LevelingUpgrades up in allUpgrades)
        {
            itterators.Add(itt);
            itt++;
        }

        int random1;
        int random2;
        int random3;

        random1 = itterators[Random.Range(0, itterators.Count)];
        itterators.Remove(random1);

        random2 = itterators[Random.Range(0, itterators.Count)];
        itterators.Remove(random2);

        random3 = itterators[Random.Range(0, itterators.Count)];

        upgradeRarity.Add(chooseRarity(allUpgrades[random1]));
        upgradeRarity.Add(chooseRarity(allUpgrades[random2]));
        upgradeRarity.Add(chooseRarity(allUpgrades[random3]));

        chosenUpgrades.Add(allUpgrades[random1]);
        chosenUpgrades.Add(allUpgrades[random2]);
        chosenUpgrades.Add(allUpgrades[random3]);

        return chosenUpgrades;
    }

    private Rarity chooseRarity(LevelingUpgrades upgrade)
    {
        float randomChance = Random.Range(0.0f, 100.0f);

        if(randomChance <= upgrade.RareChance)
        {
            return Rarity.Rare;
        }

        if(randomChance <= upgrade.UncommonChance + upgrade.RareChance)
        {
            return Rarity.Uncommon;
        }

        return Rarity.Common;
    } 
}
