using UnityEngine;
using UnityEngine.UI;

public class UISwitchWeaponPanel : MonoBehaviour
{
    [SerializeField] private Fighter _fighter;

    [SerializeField] private Button _switchWeaponButton;

    private void OnEnable()
    {
        _fighter.WeaponTryingToChanged += OnSwitchWeaponStateStarted;
        _fighter.WeaponWasChanged += OnSwitchWeaponStateFinished;
    }

    private void OnDisable()
    {
        _fighter.WeaponTryingToChanged -= OnSwitchWeaponStateStarted;
        _fighter.WeaponWasChanged -= OnSwitchWeaponStateFinished;
    }

    private void OnSwitchWeaponStateStarted()
    {
        _switchWeaponButton.gameObject.SetActive(false);
    }

    private void OnSwitchWeaponStateFinished(Weapon weapon)
    {
        _switchWeaponButton.gameObject.SetActive(true);
    }
}