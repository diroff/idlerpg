using UnityEngine;

public class FightTester : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;

    private bool _isFight = true;

    private void OnEnable()
    {
        _player.Died += OnFighterDied;
        _enemy.Died += OnFighterDied;
    }

    private void OnDisable()
    {
        _player.Died -= OnFighterDied;
        _enemy.Died -= OnFighterDied;
    }

    [ContextMenu("Fight")]
    public void Fight()
    {
        _enemy.ApplyDamage(_player.CalculateTotalDamage());
        _player.ApplyDamage(_enemy.CalculateTotalDamage());
    }

    private void OnFighterDied()
    {
        _isFight = false;
    }
}