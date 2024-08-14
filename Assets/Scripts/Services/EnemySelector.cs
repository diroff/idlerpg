using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    [SerializeField] private List<EnemyStats> _enemies;

    private List<float> _cumulativeChances;
    private List<EnemyStats> _filteredEnemies;

    private void Awake()
    {
        InitializeFilteredEnemies();
        InitializeCumulativeChances();
    }

    private void InitializeFilteredEnemies()
    {
        _filteredEnemies = new List<EnemyStats>();

        foreach (var enemy in _enemies)
        {
            if (enemy.SpawnChance <= 0f)
                continue;

            if (enemy.SpawnChance >= 1.0f)
            {
                _filteredEnemies.Clear();
                _filteredEnemies.Add(enemy);
                return;
            }

            _filteredEnemies.Add(enemy);
        }
    }

    private void InitializeCumulativeChances()
    {
        _cumulativeChances = new List<float>(_filteredEnemies.Count);

        float totalChance = 0f;

        foreach (var enemy in _filteredEnemies)
            totalChance += enemy.SpawnChance;

        float cumulativeChance = 0f;

        foreach (var enemy in _filteredEnemies)
        {
            cumulativeChance += enemy.SpawnChance / totalChance;
            _cumulativeChances.Add(cumulativeChance);
        }
    }

    public EnemyStats GetRandomEnemy()
    {
        if (_filteredEnemies.Count == 1)
            return _filteredEnemies[0];

        float randomValue = Random.Range(0f, 1f);

        for (int i = 0; i < _cumulativeChances.Count; i++)
            if (randomValue <= _cumulativeChances[i])
                return _filteredEnemies[i];

        Debug.LogError("No enemy could be selected!");

        return null;
    }

    [ContextMenu("Pick random enemy")]
    public void ShowRandomEnemyWithChance()
    {
        var enemy = GetRandomEnemy();

        Debug.Log($"{enemy.Name} : chance - {enemy.SpawnChance * 100} %");
    }
}
