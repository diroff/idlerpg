using UnityEngine;

public class CurrentWeaponUIInitializer : MonoBehaviour
{
    [SerializeField] private UICurrentWeaponDisplayer _uiCurrentWeapon;

    public void Initialize()
    {
        _uiCurrentWeapon.gameObject.SetActive(true);
        _uiCurrentWeapon.Initialize();
    }
}