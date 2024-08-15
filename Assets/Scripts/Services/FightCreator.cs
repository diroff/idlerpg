using System.Collections.Generic;
using UnityEngine;

public class FightCreator : MonoBehaviour
{
    [SerializeField] private EnemySelector _enemySelector;
    [SerializeField] private FightTester _tester;

    [SerializeField] private List<FighterBehaviour> _fighterBehaviours;

    [ContextMenu("Create Fight")]
    public void CreateFight()
    {
        var enemy = _enemySelector.GetRandomEnemy();

        _tester.Initialize(enemy);

        foreach (var fighter in _fighterBehaviours)
            fighter.PrepareToFight(_tester);

        _tester.StartFight();
    }
}