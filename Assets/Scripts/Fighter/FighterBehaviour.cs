using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FighterBehaviour : MonoBehaviour
{
    [SerializeField] private Fighter _fighter;

    [SerializeField] private bool _isDisableOnEnd;

    private Coroutine _prepareCoroutine;
    private Coroutine _attackCoroutine;

    private Fight _fight;
    private Fighter _target;

    private bool _fightIsActive = false;

    public UnityAction<float, float> PrepareTimeChanged;
    public UnityAction<float, float> AttackTimeChanged;
    public UnityAction<float, float> WeaponSwitchTimeChanged;

    public UnityAction PrepareStateStarted;
    public UnityAction AttackStateStarted;
    public UnityAction SwitchWeaponStateStarted;

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

        StopFightCoroutines();

        _fight.FightStarted -= OnFightStarted;
        _fight.FightEnded -= OnFightEnded;

        if (_isDisableOnEnd)
            _fighter.gameObject.SetActive(false);
    }

    private void StopFightCoroutines()
    {
        if(_prepareCoroutine != null)
            StopCoroutine(_prepareCoroutine);

        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);
    }

    private IEnumerator PrepareCoroutine(Fighter fighter, Fighter target)
    {
        if(!_fightIsActive)
            yield break;

        PrepareStateStarted?.Invoke();

        Debug.Log($"{Time.time}: {fighter} prepared to fight");

        var waitTime = fighter.CalculateTotalPrepareDelay();
        var maxTime = waitTime;

        while (waitTime > 0)
        {
            if (fighter.IsWeaponChanging)
            {
                Debug.Log("Weapon is changing:" + fighter.IsWeaponChanging);
                yield return StartCoroutine(SwitchWeaponCoroutine(fighter, target));
            }

            waitTime -= Time.deltaTime;
            PrepareTimeChanged?.Invoke(waitTime, maxTime);
            yield return null;
        }

        _attackCoroutine = StartCoroutine(AttackCoroutine(fighter, target));
    }

    private IEnumerator AttackCoroutine(Fighter fighter, Fighter target)
    {
        if (!_fightIsActive) yield break;

        Debug.Log($"{Time.time}: {fighter} prepared to attack");

        AttackStateStarted?.Invoke();

        var waitTime = fighter.CalculateAttackDelay();
        var maxTime = waitTime;

        while (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            AttackTimeChanged?.Invoke(waitTime, maxTime);
            yield return null;
        }

        if (!_fightIsActive) yield break;

        target.ApplyDamage(fighter.CalculateTotalDamage());

        if (fighter.IsWeaponChanging)
            yield return StartCoroutine(SwitchWeaponCoroutine(fighter, target));

        if (!_fightIsActive) yield break;

        _prepareCoroutine = StartCoroutine(PrepareCoroutine(fighter, target));
    }

    private IEnumerator SwitchWeaponCoroutine(Fighter fighter, Fighter target)
    {
        if (!_fightIsActive) yield break;

        SwitchWeaponStateStarted?.Invoke();

        var waitTime = fighter.TryingWeapon.CalculateTotalEquipTime();
        var maxTime = waitTime;

        while(waitTime > 0)
        {
            waitTime -= Time.deltaTime;

            WeaponSwitchTimeChanged?.Invoke(waitTime, maxTime);
            yield return null;
        }

        fighter.SetWeapon();
    }
}