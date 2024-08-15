using UnityEngine;

public class WeaponSwitchListener : FightListener
{
    [SerializeField] private Fighter _fighter;

    private bool _isFightActive = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        _fighter.WeaponTryingToChanged += SwitchWeapon;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _fighter.WeaponTryingToChanged -= SwitchWeapon;
    }

    private void SwitchWeapon()
    {
        if (_isFightActive)
            return;

        _fighter.SetWeapon();
    }

    protected override void OnFightStateChangedAction(bool isFightActive)
    {
        _isFightActive = isFightActive;
    }
}