using UnityEngine;

public class Player : Fighter
{
    [SerializeField] protected MeleeWeapon StartMeleeWeapon;
    [SerializeField] protected RangedWeapon StartRangedWeapon;

    [ContextMenu("Take melee weapon")]
    public void TakeMeleeWeapon()
    {
        TryToSetWeapon(StartMeleeWeapon);
    }

    [ContextMenu("Take ranged weapon")]
    public void TakeRangedWeapon()
    {
        TryToSetWeapon(StartRangedWeapon);
    }
}
