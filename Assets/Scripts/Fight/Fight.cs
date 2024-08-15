using UnityEngine;
using UnityEngine.Events;

public class Fight : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemyPlug;

    public UnityAction FightStarted;
    public UnityAction FightEnded;

    public void Initialize(EnemyStats enemyStats)
    {
        _player.Died += OnFighterDied;

        _enemyPlug.gameObject.SetActive(true);
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

    public void StartFight()
    {
        FightStarted?.Invoke();
    }

    public Fighter GetTarget(Fighter fighter)
    {
        return fighter == _player ? _enemyPlug : _player;
    }

    private void OnFighterDied()
    {
        StopFight();
    }
}