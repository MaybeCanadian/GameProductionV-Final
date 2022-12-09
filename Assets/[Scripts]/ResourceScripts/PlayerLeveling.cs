using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLeveling : MonoBehaviour
{
    static public PlayerLeveling instance;

    public delegate void OnLevelUp();
    public static OnLevelUp onLevelUp;

    [Header("Xp values")]
    [Tooltip("The current XP of the player")]
    public float XP;
    public int Level;
    public float XPRequiredToLevel;
    public float XPRequiredIncreaseFactor = 0.2f;

    [Header("UI references")]
    public Slider XPslider;
    public Text LevelText;
    
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

    private void Start()
    {
        XP = 0;
        Level = 1;

        CheckLevel();
    }

    public void GainXP(float input)
    {
        XP += input;

        CheckLevel();
    }

    private void CheckLevel()
    {
        if(XP >= XPRequiredToLevel)
        {
            LevelUp();
        }

        if(XPRequiredToLevel != 0)
            XPslider.value = XP / XPRequiredToLevel;
    }

    private void LevelUp()
    {
        XP -= XPRequiredToLevel;
        XPRequiredToLevel += XPRequiredToLevel * XPRequiredIncreaseFactor;
        Level++;
        LevelText.text = Level.ToString();

        onLevelUp?.Invoke();
    }
}
