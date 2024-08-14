using UnityEngine;

public class Player : Fighter
{
    [SerializeField] protected MeleeWeapon StartMeleeWeapon;
    [SerializeField] protected RangedWeapon StartRangedWeapon;

    [ContextMenu("Take melee weapon")]
    public void TakeMeleeWeapon()
    {
        SetWeapon(StartMeleeWeapon);
    }

    [ContextMenu("Take ranged weapon")]
    public void TakeRangedWeapon()
    {
        SetWeapon(StartRangedWeapon);
    }
}
