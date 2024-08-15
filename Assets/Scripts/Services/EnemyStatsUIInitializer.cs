using UnityEngine;

public class EnemyStatsUIInitializer : MonoBehaviour
{
    [SerializeField] private Fight _fight;
    [SerializeField] private GameObject _enemyStatsPanel;

    [Header("UI Initializers")]
    [SerializeField] private HealthUIInitializer _healthUIInitializer;
    [SerializeField] private CurrentWeaponUIInitializer _currentWeaponUIInitializer;

    private void OnEnable()
    {
        _fight.FightStarted += OnFightStarted;
        _fight.FightEnded += OnFightEnded;
    }

    private void OnDisable()
    {
        _fight.FightStarted -= OnFightStarted;
        _fight.FightEnded -= OnFightEnded;
    }

    private void OnFightStarted()
    {
        _enemyStatsPanel.SetActive(true);
        _currentWeaponUIInitializer.Initialize();
        _healthUIInitializer.Initialize();
    }

    private void OnFightEnded()
    {
        _enemyStatsPanel.SetActive(false);
    }
}