using System.Collections;
using UnityEngine;

public class FightTester : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;

    private bool _fightIsActive;

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
        _fightIsActive = true;

        StartCoroutine(PlayerAttack());
        StartCoroutine(EnemyAttack());

        while (_fightIsActive)
        {
            yield return null;
        }
    }

    private IEnumerator PlayerAttack()
    {
        while (_fightIsActive)
        {
            _enemy.ApplyDamage(_player.CalculateTotalDamage());

            if (!_fightIsActive)
                yield break;

            yield return new WaitForSeconds(_player.CalculateAttackDelay());
        }
    }

    private IEnumerator EnemyAttack()
    {
        while (_fightIsActive)
        {
            _player.ApplyDamage(_enemy.CalculateTotalDamage());

            if (!_fightIsActive)
                yield break;

            yield return new WaitForSeconds(_enemy.CalculateAttackDelay());
        }
    }

    private void OnFighterDied()
    {
        _fightIsActive = false;
        Debug.Log("Fight has ended.");

        StopCoroutine(PlayerAttack());
        StopCoroutine(EnemyAttack());
        StopCoroutine(Fight());
    }
}