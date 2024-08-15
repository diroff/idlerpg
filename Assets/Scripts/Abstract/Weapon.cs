using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected WeaponStats Stats;

    public void Initialize(WeaponStats stats)
    {
        Stats = stats;
    }

    public WeaponStats GetStats()
    {
        return Stats;
    }

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