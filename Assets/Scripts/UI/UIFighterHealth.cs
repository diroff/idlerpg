using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFighterHealth : MonoBehaviour
{
    [SerializeField] private Image _healthSpirte;
    [SerializeField] private TextMeshProUGUI _healthText;

    [SerializeField] private Fighter _fighter;

    private void OnEnable()
    {
        if (_fighter == null)
            return;

        _fighter.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        if (_fighter == null)
            return;

        _fighter.HealthChanged += OnHealthChanged;
    }

    public void Initialize(Fighter fighter)
    {
        _fighter = fighter;
        _fighter.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int currentValue, int maxValue)
    {
        _healthText.text = $"{currentValue}/{maxValue}";
        _healthSpirte.fillAmount = (float)currentValue / maxValue;
    }
}