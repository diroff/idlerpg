using UnityEngine;
using UnityEngine.UI;

public class UICurrentWeaponDisplayer : MonoBehaviour
{
    [SerializeField] private Image _weaponIcon;
    [SerializeField] private Fighter _fighter;

    private WeaponStats _currentWeaponStats;

    private void OnEnable()
    {
        if (_fighter == null)
            return;

        Initialize();
    }

    private void OnDisable()
    {
        _fighter.WeaponWasChanged -= OnWeaponChanged;
    }

    public void Initialize()
    {
        _weaponIcon.gameObject.SetActive(true);

        _fighter.WeaponWasChanged += OnWeaponChanged;
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        _currentWeaponStats = weapon.GetStats();

        _weaponIcon.sprite = _currentWeaponStats.Icon;
    }
}