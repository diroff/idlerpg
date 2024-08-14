using UnityEngine;
using UnityEngine.Events;

public abstract class Fighter : MonoBehaviour, IDamageable
{
    [SerializeField] protected CharacterStats Stats;
    [SerializeField] protected Weapon CurrentWeapon;

    protected int CurrentHealth;

    public UnityAction<int, int> HealthChanged;
    public UnityAction Died;

    private void Start()
    {
        Initialize();
    }

    public void ApplyDamage(int damage)
    {
        damage = ReduceDamageByArmor(damage);

        if (damage < 0)
            Debug.LogError("Damage < 0");

        CurrentHealth -= damage;

        if(CurrentHealth < 0)
            CurrentHealth = 0;

        HealthChanged?.Invoke(CurrentHealth, Stats.MaxHealth);

        if (IsDead())
            Die();

        Debug.Log($"{Stats.Name} take {damage} damage, current health: {CurrentHealth}");
    }

    public virtual int CalculateTotalDamage()
    {
        int weaponDamage = CurrentWeapon != null ? CurrentWeapon.CalculateTotalDamage() : 0;
        return Stats.BaseAttackPower + weaponDamage;
    }

    public virtual float CalculateTotalPrepareDelay()
    {
        return Stats.BaseAttackDelay;
    }

    public virtual float CalculateAttackDelay()
    {
        return CurrentWeapon != null ? CurrentWeapon.CalculateTotalAttackDelay() : 0;
    }

    protected virtual int ReduceDamageByArmor(int damage)
    {
        int totalDamage = damage -= Stats.Armor;

        if(totalDamage < 0)
            totalDamage = 0;

        return totalDamage;
    }

    protected virtual void Initialize()
    {
        CurrentHealth = Stats.MaxHealth;
        HealthChanged?.Invoke(CurrentHealth, Stats.MaxHealth);
    }

    protected virtual bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    protected virtual void Die()
    {
        Debug.Log($"{Stats.Name} is die");
        Died?.Invoke();
    }
}