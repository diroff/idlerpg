using UnityEngine;
using UnityEngine.Events;

public class FightTester : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemyPlug;

    public UnityAction FightStarted;
    public UnityAction FightEnded;

    public void Initialize(EnemyStats enemyStats)
    {
        _player.Died += OnFighterDied;
        _enemyPlug.Initialize(enemyStats);
        _enemyPlug.Died += OnFighterDied;
    }

    private void StopFight()
    {
        _player.Died -= OnFighterDied;
        _enemyPlug.Died -= OnFighterDied;

        Debug.Log("Fight has ended.");
        FightEnded?.Invoke();
    }

    [ContextMenu("Start Fight")]
    public void StartFight()
    {
        Fight();
    }

    public Fighter GetTarget(Fighter fighter)
    {
        return fighter == _player ? _enemyPlug : _player;
    }

    private void Fight()
    {
        FightStarted?.Invoke();
    }

    private void OnFighterDied()
    {
        StopFight();
    }
}