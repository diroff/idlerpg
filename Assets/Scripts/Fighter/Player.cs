using UnityEngine;

public class Player : Fighter
{
    [Header("Player parameters")]
    [SerializeField] protected RangedWeaponStats StartRangedWeapon;

    [ContextMenu("Take melee weapon")]
    public void TakeMeleeWeapon()
    {
        TryToSetWeapon(Stats.StartWeapon);
    }

    [ContextMenu("Take ranged weapon")]
    public void TakeRangedWeapon()
    {
        TryToSetWeapon(StartRangedWeapon);
    }
}
