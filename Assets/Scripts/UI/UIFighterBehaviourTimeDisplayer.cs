using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFighterBehaviourTimeDisplayer : MonoBehaviour
{
    [SerializeField] private FighterBehaviour _fighter;

    [SerializeField] private Image _prepareImage;
    [SerializeField] private Image _attackImage;
    [SerializeField] private Image _weaponSwitchImage;

    [SerializeField] private TextMeshProUGUI _prepareTimeText;
    [SerializeField] private TextMeshProUGUI _attackTimeText;
    [SerializeField] private TextMeshProUGUI _weaponSwitchTimeText;

    private void OnEnable()
    {
        _fighter.PrepareTimeChanged += (currentTime, maxTime) => UpdateTimeText(_prepareTimeText, _prepareImage, currentTime, maxTime);
        _fighter.AttackTimeChanged += (currentTime, maxTime) => UpdateTimeText(_attackTimeText, _attackImage, currentTime, maxTime);
        _fighter.WeaponSwitchTimeChanged += (currentTime, maxTime) => UpdateTimeText(_weaponSwitchTimeText, _weaponSwitchImage, currentTime, maxTime);
    }

    private void OnDisable()
    {
        _fighter.PrepareTimeChanged -= (currentTime, maxTime) => UpdateTimeText(_prepareTimeText, _prepareImage, currentTime, maxTime);
        _fighter.AttackTimeChanged -= (currentTime, maxTime) => UpdateTimeText(_attackTimeText, _attackImage, currentTime, maxTime);
        _fighter.WeaponSwitchTimeChanged -= (currentTime, maxTime) => UpdateTimeText(_weaponSwitchTimeText, _weaponSwitchImage, currentTime, maxTime);
    }

    private void UpdateTimeText(TextMeshProUGUI textComponent, Image image, float currentTime, float maxTime)
    {
        textComponent.text = $"{Math.Round(currentTime, 1)}/{maxTime}";
        image.fillAmount = currentTime / maxTime;
    }
}