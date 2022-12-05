using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUIControllerScript : MonoBehaviour
{
    public GameObject levelUpUI;

    public List<LevelUpSlotScript> levelSlots;

    public List<LevelingUpgrades> chosenUpgrades;

    private void OnEnable()
    {
        PlayerLeveling.onLevelUp += OnLevelUp;
    }

    private void OnDisable()
    {
        PlayerLeveling.onLevelUp -= OnLevelUp;
    }

    public void OnLevelUp()
    {
        Time.timeScale = 0.0f;
        GameStateMachine.GetInstance().ChangeState(GameStates.UPGRADE);
        levelUpUI.SetActive(true);

        UpdateSlots();
    }

    private void UpdateSlots()
    {
        List<LevelingUpgrades> upgrades = UpgradeChosingScript.instance.ChooseThreeUpgrades(out List<Rarity> upgradeRarity);

        int itt = 0;
        foreach (LevelUpSlotScript slot in levelSlots)
        {
            slot.UpdateSlot(upgrades[itt], upgradeRarity[itt]);
            itt++;
        }
    }

    public void OnSlotButtonPressed(LevelingUpgrades upgrade, Rarity rarity)
    {
        upgrade.PickUpgrade(rarity);

        CloseUI();
    }

    private void CloseUI()
    {
        levelUpUI.SetActive(false);
        GameStateMachine.GetInstance().ChangeState(GameStates.GAME);
        Time.timeScale = 1.0f;
    }
}
