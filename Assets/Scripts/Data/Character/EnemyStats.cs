using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "RPG/Fighter Data/New Enemy Data")]
public class EnemyStats : CharacterStats
{
    [field: Header("Enemy parameters")]
    [field: SerializeField, Range(0, 1)] public float SpawnChance { get; private set; }
}