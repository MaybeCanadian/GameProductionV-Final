using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputController : MonoBehaviour
{
    public PlayerMovementController playerMovement;
    public PlayerCombatScript playerCombat;
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();

        playerMovement.InputMove(movement);
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            playerMovement.Jump();
        }
    }
    public void OnMainButtonInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if (GameStateMachine.GetInstance().GetState() == GameStates.GAME)
            {
                playerCombat.AttackButtonDown();
            }
            if(GameStateMachine.GetInstance().GetState() == GameStates.BUILDING)
            {
                BuildingManagerScript.instance.OnMainButtonPressed();
            }
        }
    }
    public void OnOffButtonuInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            playerCombat.OffButtonDown();
        }
    }
    public void OnSwitchButton1(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            playerCombat.ChangeWeaponSlot(0);
        }
    }
    public void OnSwitchButton2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerCombat.ChangeWeaponSlot(1);
        }
    }
    public void OnSwitchButton3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerCombat.ChangeWeaponSlot(2);
        }
    }
    public void OnSwitchButton4(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerCombat.ChangeWeaponSlot(3);
        }
    }
}
