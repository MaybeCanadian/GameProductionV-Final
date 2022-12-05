using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    public Animator animator;

    public void PlayAttackAnimation(WeaponTypes type)
    {
        switch (type)
        {
            case WeaponTypes.Sword:
                {
                    animator.SetTrigger("OneHandedAttack");
                    break;
                }
            case WeaponTypes.Bow:
                {
                    animator.SetTrigger("BowAttack");
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void Die()
    {
        animator.SetTrigger("Death");
    }
}
