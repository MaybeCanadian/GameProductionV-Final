using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatScript : MonoBehaviour
{
    public WeaponItems currentWeapon;
    public WeaponItems[] equipedWeapons = new WeaponItems[4];
    public int equipedItt = 0;
    public GameObject BodyObject;
    public bool IsAttacking = false;
    public PlayerAnimationScript playerAnims;

    public AudioSource attackSoundsSource;

    private void Start()
    {
        IsAttacking = false;
        currentWeapon = equipedWeapons[equipedItt];
    }

    public void AttackButtonDown()
    {
        if (currentWeapon != null && IsAttacking == false)
        {
            IsAttacking = true;
            playerAnims.PlayAttackAnimation(currentWeapon.weaponType);
        }
    } //these call the current weapons main and off functions

    public void OffButtonDown()
    {
    }

    public void OnAttackEvent(AttackEvents type)
    {
        if (IsAttacking == true)
        {
            IsAttacking = false;
            currentWeapon.attack(BodyObject.transform);
            attackSoundsSource.PlayOneShot(SoundManager.instance.GetFXClip(currentWeapon.soundEffect), 0.3f);
        }
    }

    public void ChangeWeaponSlot(int slot)
    {
        if (IsAttacking == false)
            if (equipedWeapons.Length >= slot)
                if (equipedWeapons[slot] != null && equipedItt != slot)
                {
                    equipedItt = slot;
                    currentWeapon = equipedWeapons[equipedItt];
                }
    }
}
