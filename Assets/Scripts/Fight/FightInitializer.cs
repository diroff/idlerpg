using System.Collections.Generic;
using UnityEngine;

public class FightInitializer : MonoBehaviour
{
    [SerializeField] private EnemySelector _enemySelector;
    [SerializeField] private Fight _fight;

    [SerializeField] private List<FighterBehaviour> _fighterBehaviours;

    [ContextMenu("Create Fight")]
    public void CreateFight()
    {
        var enemy = _enemySelector.GetRandomEnemy();

        _fight.Initialize(enemy);

        foreach (var fighter in _fighterBehaviours)
            fighter.PrepareToFight(_fight);

        _fight.StartFight();
    }

    public void StopFight()
    {
        _fight.StopFight();
    }

    public void ChangeFightState()
    {
        if (_fight.FightEnabled)
            StopFight();
        else
            CreateFight();
    }
}