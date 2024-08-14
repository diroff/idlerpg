using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponStats Stats;

    public virtual int CalculateTotalDamage()
    {
        return Stats.BaseAttackPower;
    }

    public virtual float CalculateTotalAttackDelay()
    {
        return Stats.BaseAttackDelay;
    }

    public virtual float CalculateTotalEquipTime()
    {
        return Stats.EquipDelay;
    }
}