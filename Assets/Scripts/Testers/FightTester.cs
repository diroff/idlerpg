using System.Collections;
using UnityEngine;

public class FightTester : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;

    private bool _fightIsActive;

    private Coroutine _playerPrepareCoroutine;
    private Coroutine _enemyPrepareCoroutine;

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

    [ContextMenu("Start Fight")]
    public void StartFight()
    {
        StartCoroutine(Fight());
    }

    private IEnumerator Fight()
    {
        Debug.Log("Fight is started");

        _fightIsActive = true;

        _playerPrepareCoroutine = StartCoroutine(PrepareCoroutine(_player, _enemy, _playerPrepareCoroutine));
        _enemyPrepareCoroutine = StartCoroutine(PrepareCoroutine(_enemy, _player, _enemyPrepareCoroutine));

        while (_fightIsActive)
            yield return null;

        DisableCoroutine(_playerPrepareCoroutine);
        DisableCoroutine(_enemyPrepareCoroutine);

        Debug.Log("Fight has ended.");
    }

    private IEnumerator PrepareCoroutine(Fighter fighter, Fighter target, Coroutine coroutine)
    {
        if (!_fightIsActive)
            yield break;

        yield return new WaitForSeconds(fighter.CalculateTotalPrepareDelay());

        coroutine = StartCoroutine(AttackCoroutine(fighter, target, coroutine));
    }

    private IEnumerator AttackCoroutine(Fighter fighter, Fighter target, Coroutine coroutine)
    {
        if (!_fightIsActive)
            yield break;

        yield return new WaitForSeconds(fighter.CalculateAttackDelay());

        target.ApplyDamage(fighter.CalculateTotalDamage());
        coroutine = StartCoroutine(PrepareCoroutine(fighter, target, coroutine));
    }

    private void OnFighterDied()
    {
        _fightIsActive = false;
    }

    private void DisableCoroutine(Coroutine coroutine)
    {
        if (coroutine == null)
            return;

        StopCoroutine(coroutine);
        coroutine = null;
    }
}