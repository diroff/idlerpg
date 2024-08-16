using UnityEngine;
using UnityEngine.Events;

public class Fight : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemyPlug;

    public UnityAction FightStarted;
    public UnityAction FightEnded;

    public UnityAction FightEndedWithEnemyLoose;

    public bool FightEnabled {get; private set;}

    public void Initialize(EnemyStats enemyStats)
    {
        _player.Died += OnPlayerDied;

        _enemyPlug.gameObject.SetActive(true);
        _enemyPlug.Initialize(enemyStats);
        _enemyPlug.Died += OnEnemyDied;
    }

    public void StopFight()
    {
        FightEnabled = false;

        _player.Died -= OnPlayerDied;
        _enemyPlug.Died -= OnEnemyDied;

        Debug.Log("Fight has ended.");
        FightEnded?.Invoke();
    }

    public void StartFight()
    {
        FightStarted?.Invoke();

        FightEnabled = true;
    }

    public Fighter GetTarget(Fighter fighter)
    {
        return fighter == _player ? _enemyPlug : _player;
    }

    private void OnPlayerDied()
    {
        StopFight();
    }

    private void OnEnemyDied()
    {
        StopFight();
        FightEndedWithEnemyLoose?.Invoke();
    }
}