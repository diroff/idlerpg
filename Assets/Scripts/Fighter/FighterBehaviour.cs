using System.Collections;
using UnityEngine;

public class FighterBehaviour : MonoBehaviour
{
    [SerializeField] private Fighter _fighter;

    private Coroutine _prepareCoroutine;
    private Coroutine _attackCoroutine;

    private Fight _fight;
    private Fighter _target;

    private bool _fightIsActive = false;

    private void OnDisable()
    {
        if (_fight == null)
            return;

        _fight.FightStarted -= OnFightStarted;
        _fight.FightEnded -= OnFightEnded;
    }

    public void PrepareToFight(Fight fight)
    {
        _fight = fight;

        _fight.FightStarted += OnFightStarted;
        _fight.FightEnded += OnFightEnded;

        _target = _fight.GetTarget(_fighter);
    }

    private void StartFight()
    {
        _fightIsActive = true;

        _prepareCoroutine = StartCoroutine(PrepareCoroutine(_fighter, _target));
    }

    private void OnFightStarted()
    {
        StartFight();
    }

    private void OnFightEnded()
    {
        _fightIsActive = false;

        StopCoroutine(_prepareCoroutine);
        StopCoroutine(_attackCoroutine);

        _fight.FightStarted -= OnFightStarted;
        _fight.FightEnded -= OnFightEnded;
    }

    private IEnumerator PrepareCoroutine(Fighter fighter, Fighter target)
    {
        if(!_fightIsActive)
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

        _attackCoroutine = StartCoroutine(AttackCoroutine(fighter, target));
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

        if (!_fightIsActive)
            yield break;

        target.ApplyDamage(fighter.CalculateTotalDamage());

        if (fighter.IsWeaponChanging)
        {
            yield return new WaitForSeconds(fighter.TryingWeapon.CalculateTotalEquipTime());
            fighter.SetWeapon();
        }

        _prepareCoroutine = StartCoroutine(PrepareCoroutine(fighter, target));
    }
}