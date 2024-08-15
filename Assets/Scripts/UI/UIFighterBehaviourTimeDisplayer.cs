using System;
using TMPro;
using UnityEngine;

public class UIFighterBehaviourTimeDisplayer : MonoBehaviour
{
    [SerializeField] private FighterBehaviour _fighter;

    [SerializeField] private TextMeshProUGUI _prepareTimeText;
    [SerializeField] private TextMeshProUGUI _attackTimeText;
    [SerializeField] private TextMeshProUGUI _weaponSwtichTimeText;

    private void OnEnable()
    {
        _fighter.PrepareTimeChanged += ShowPrepareTime;
        _fighter.AttackTimeChanged += ShowAttackTime;
        _fighter.WeaponSwitchTimeChanged += ShowSwitchWeaponTime;
    }

    private void OnDisable()
    {
        _fighter.PrepareTimeChanged -= ShowPrepareTime;
        _fighter.AttackTimeChanged -= ShowAttackTime;
        _fighter.WeaponSwitchTimeChanged -= ShowSwitchWeaponTime;
    }

    private void ShowPrepareTime(float currentTime, float maxTime)
    {
        _prepareTimeText.text = $"{Math.Round(currentTime, 2)}/{maxTime}";
    }

    private void ShowAttackTime(float currentTime, float maxTime)
    {
        _attackTimeText.text = $"{Math.Round(currentTime, 2)}/{maxTime}";
    }

    private void ShowSwitchWeaponTime(float currentTime, float maxTime)
    {
        _weaponSwtichTimeText.text = $"{Math.Round(currentTime, 2)}/{maxTime}";
    }
}