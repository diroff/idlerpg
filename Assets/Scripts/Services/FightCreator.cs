using UnityEngine;

public class FightCreator : MonoBehaviour
{
    [SerializeField] private EnemySelector _enemySelector;
    [SerializeField] private FightTester _tester;

    [ContextMenu("Create Fight")]
    public void CreateFight()
    {
        var enemy = _enemySelector.GetRandomEnemy();
        _tester.Initialize(enemy);
        _tester.StartFight();
    }
}