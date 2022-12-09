using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimGoBetween : MonoBehaviour
{
    public PlayerCombatScript combatScript;
    public PlayerAudioScript audioScript;

    public void OnOneHandedAttackEvent()
    {
        combatScript.OnAttackEvent(AttackEvents.ONHANDED);
    }

    public void OnBowAttackEvent()
    {
        combatScript.OnAttackEvent(AttackEvents.BOW);
    }
    public void OnRunEvent()
    {
        audioScript.PlayRunSound();
    }
}

public enum AttackEvents {
    ONHANDED,
    BOW
}
