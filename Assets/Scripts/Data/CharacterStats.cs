using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "RPG/New Fighter Data")]
public class CharacterStats : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField, Min(1)] public int MaxHealth { get; private set; }
    [field: SerializeField, Min(0)] public int Armor { get; private set; }
    [field: SerializeField, Min(0)] public int BaseAttackPower { get; private set; }
}