using UnityEngine;

public class FightPanelUIInstaller : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _fightButtonsPanel;

    private void OnEnable()
    {
        _player.HealthChanged += OnPlayerHealthChange;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnPlayerHealthChange;
    }

    private void OnPlayerHealthChange(int health, int maxHealth)
    {
        if (health <= 0)
            _fightButtonsPanel.SetActive(false);
        else
            _fightButtonsPanel.SetActive(true);
    }
}