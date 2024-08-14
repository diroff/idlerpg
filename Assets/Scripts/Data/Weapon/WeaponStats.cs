using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "RPG/Weapon Data/New Weapon Data")]
public class WeaponStats : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }

    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    [field: SerializeField, Min(0)] public int BaseAttackPower { get; private set; }
    [field: SerializeField, Min(0)] public float BaseAttackDelay { get; private set; }
}