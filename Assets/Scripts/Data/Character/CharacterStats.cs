using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "RPG/Fighter Data/New Fighter Data")]
public class CharacterStats : ScriptableObject
{
    [field: Header("Character info")]
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }

    [field: Header("Stats")]
    [field: SerializeField, Min(1)] public int MaxHealth { get; private set; }
    [field: SerializeField, Min(0)] public int Armor { get; private set; }
    [field: SerializeField, Min(0)] public int BaseAttackPower { get; private set; }
    [field: SerializeField, Min(0)] public float BaseAttackDelay { get; private set; }

    [field: Header("Equipment")]
    [field: SerializeField, Min(0)] public WeaponStats StartWeapon { get; private set; }
}