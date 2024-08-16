public class Player : Fighter
{
    private PlayerStats PlayerStats => (PlayerStats)Stats;

    private RangedWeaponStats _currentRangedWeapon;

    public override void Initialize(CharacterStats stats)
    {
        base.Initialize(stats);

        _currentRangedWeapon = PlayerStats.StartRangedWeapon;
    }

    private void TakeMeleeWeapon()
    {
        TryToSetWeapon(Stats.StartWeapon);
    }

    private void TakeRangedWeapon()
    {
        TryToSetWeapon(_currentRangedWeapon);
    }

    public void SwitchWeapon()
    {
        if (CurrentWeapon == _currentRangedWeapon)
            TakeMeleeWeapon();
        else
            TakeRangedWeapon();
    }
}