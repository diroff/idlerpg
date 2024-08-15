using System.Collections;
using UnityEngine;

public class FightTester : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemyPlug;

    private bool _fightIsActive;

    public void Initialize(EnemyStats enemyStats)
    {
        _player.Died += OnFighterDied;
        _enemyPlug.Initialize(enemyStats);
        _enemyPlug.Died += OnFighterDied;
    }

    private void StopFight()
    {
        _fightIsActive = false;
        _player.Died -= OnFighterDied;
        _enemyPlug.Died -= OnFighterDied;

        Debug.Log("Fight has ended.");
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

        StartCoroutine(PrepareCoroutine(_player, _enemyPlug));
        StartCoroutine(PrepareCoroutine(_enemyPlug, _player));

        while (_fightIsActive)
            yield return null;
    }

    private IEnumerator PrepareCoroutine(Fighter fighter, Fighter target)
    {
        if (!_fightIsActive)
            yield break;

        Debug.Log($"{Time.time}: {fighter} prepared to fight");

        var waitTime = fighter.CalculateTotalPrepareDelay();

        while (waitTime > 0)
        {
            if (fighter.IsWeaponChanging)
            {
                yield return new WaitForSeconds(fighter.TryingWeapon.CalculateTotalEquipTime());
                fighter.SetWeapon();
            }

            waitTime -= Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(AttackCoroutine(fighter, target));
    }

    private IEnumerator AttackCoroutine(Fighter fighter, Fighter target)
    {
        if (!_fightIsActive)
            yield break;

        Debug.Log($"{Time.time}: {fighter} prepared to attack");

        var waitTime = fighter.CalculateAttackDelay();

        while (waitTime > 0)
        {
            if (!_fightIsActive)
                yield break;

            waitTime -= Time.deltaTime;
            yield return null;
        }


        target.ApplyDamage(fighter.CalculateTotalDamage());

        if (fighter.IsWeaponChanging)
        {
            yield return new WaitForSeconds(fighter.TryingWeapon.CalculateTotalEquipTime());
            fighter.SetWeapon();
        }

        yield return StartCoroutine(PrepareCoroutine(fighter, target));
    }

    private void OnFighterDied()
    {
        StopFight();
    }
}