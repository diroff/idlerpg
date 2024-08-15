using System;
using TMPro;
using UnityEngine;

public class UIFighterBehaviourTimeDisplayer : MonoBehaviour
{
    [SerializeField] private FighterBehaviour _fighter;

    [SerializeField] private TextMeshProUGUI _prepareTimeText;
    [SerializeField] private TextMeshProUGUI _attackTimeText;

    private void OnEnable()
    {
        _fighter.PrepareTimeChanged += ShowPrepareTime;
        _fighter.AttackTimeChanged += ShowAttackTime;
    }

    private void OnDisable()
    {
        _fighter.PrepareTimeChanged -= ShowPrepareTime;
        _fighter.AttackTimeChanged -= ShowAttackTime;
    }

    private void ShowPrepareTime(float currentTime, float maxTime)
    {
        _prepareTimeText.text = $"{Math.Round(currentTime, 2)}/{maxTime}";
    }

    private void ShowAttackTime(float currentTime, float maxTime)
    {
        _attackTimeText.text = $"{Math.Round(currentTime, 2)}/{maxTime}";
    }
}