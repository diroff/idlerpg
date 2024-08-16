using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightInitializer : MonoBehaviour
{
    [SerializeField] private EnemySelector _enemySelector;
    [SerializeField] private Fight _fight;

    [SerializeField] private List<FighterBehaviour> _fighterBehaviours;

    [SerializeField] private float _delayBetweenFights;

    private void OnEnable()
    {
        _fight.FightEndedWithEnemyLoose += SetNextFight;
    }

    private void OnDisable()
    {
        _fight.FightEndedWithEnemyLoose -= SetNextFight;
    }

    public void CreateFight()
    {
        var enemy = _enemySelector.GetRandomEnemy();

        _fight.Initialize(enemy);

        foreach (var fighter in _fighterBehaviours)
            fighter.PrepareToFight(_fight);

        _fight.StartFight();
    }

    private void SetNextFight()
    {
        StartCoroutine(CreateFightWithDelay());
    }

    private IEnumerator CreateFightWithDelay()
    {
        yield return new WaitForSeconds(_delayBetweenFights);
        CreateFight();
    }

    public void StopFight()
    {
        _fight.StopFight();
    }
}