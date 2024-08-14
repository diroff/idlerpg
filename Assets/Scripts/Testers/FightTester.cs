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
        _player.WeaponTryingToChanged += OnWeaponChanged;
        _enemy.Died += OnFighterDied;
        _enemy.WeaponTryingToChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _player.Died -= OnFighterDied;
        _player.WeaponTryingToChanged -= OnWeaponChanged;
        _enemy.Died -= OnFighterDied;
        _enemy.WeaponTryingToChanged -= OnWeaponChanged;
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

        _playerPrepareCoroutine = StartCoroutine(PrepareCoroutine(_player, _enemy));
        _enemyPrepareCoroutine = StartCoroutine(PrepareCoroutine(_enemy, _player));

        while (_fightIsActive)
            yield return null;

        StopCoroutine(_playerPrepareCoroutine);
        StopCoroutine(_enemyPrepareCoroutine);

        Debug.Log("Fight has ended.");
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
        _fightIsActive = false;
    }

    private void OnWeaponChanged(Fighter fighter, Weapon weapon)
    {

    }
}