using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "RPG/Fighter Data/New Player Data")]
public class PlayerStats : CharacterStats
{
    [field: SerializeField, Min(0)] public RangedWeaponStats StartRangedWeapon { get; private set; }
}
