using UnityEngine;

public class Player : Fighter
{
    private PlayerStats PlayerStats => (PlayerStats)Stats;

    private RangedWeaponStats _currentRangedWeapon;

    public override void Initialize(CharacterStats stats)
    {
        base.Initialize(stats);

        _currentRangedWeapon = PlayerStats.StartRangedWeapon;
    }

    [ContextMenu("Take melee weapon")]
    public void TakeMeleeWeapon()
    {
        TryToSetWeapon(Stats.StartWeapon);
    }

    [ContextMenu("Take ranged weapon")]
    public void TakeRangedWeapon()
    {
        TryToSetWeapon(_currentRangedWeapon);
    }
}