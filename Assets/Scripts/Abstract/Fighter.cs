using UnityEngine;
using UnityEngine.Events;

public abstract class Fighter : MonoBehaviour, IDamageable
{
    [SerializeField] protected CharacterStats Stats;
    [SerializeField] protected SpriteRenderer SpriteRenderer;

    [Header("Weapon")]
    [SerializeField] protected Weapon WeaponPlug;

    protected int CurrentHealth;

    private WeaponStats _currentWeapon;

    public UnityAction<int, int> HealthChanged;
    public UnityAction<Weapon> WeaponWasChanged;

    public UnityAction FighterWasInitialized;
    public UnityAction WeaponTryingToChanged;
    public UnityAction Died;

    public WeaponStats TryingWeapon { get; private set; }
    public bool IsWeaponChanging { get; private set; }

    protected virtual void Start()
    {
        if (Stats == null)
            return;

        Initialize(Stats);
    }

    public virtual void Initialize(CharacterStats stats)
    {
        Stats = stats;

        SpriteRenderer.sprite = stats.Sprite;
        CurrentHealth = Stats.MaxHealth;

        TryToSetWeapon(Stats.StartWeapon);
        SetWeapon();

        HealthChanged?.Invoke(CurrentHealth, Stats.MaxHealth);
    }

    public void ApplyDamage(int damage)
    {
        damage = ReduceDamageByArmor(damage);

        if (damage < 0)
        {
            Debug.LogError("Damage < 0");
            return;
        }

        CurrentHealth -= damage;

        if(CurrentHealth < 0)
            CurrentHealth = 0;

        HealthChanged?.Invoke(CurrentHealth, Stats.MaxHealth);

        if (IsDead())
            Die();

        Debug.Log($"{Time.time}: {Stats.Name} take {damage} damage, current health: {CurrentHealth}");
    }

    public void ApplyHeal(int value)
    {
        if (value < 0)
        {
            Debug.LogError("Heal value < 0");
            return;
        }

        CurrentHealth += value;

        if(CurrentHealth > Stats.MaxHealth)
            CurrentHealth = Stats.MaxHealth;

        HealthChanged?.Invoke(CurrentHealth, Stats.MaxHealth);
    }

    public void SetMaxHealth()
    {
        ApplyHeal(Stats.MaxHealth);
    }

    public void SetWeapon()
    {
        _currentWeapon = TryingWeapon;
        WeaponPlug.Initialize(_currentWeapon);
        IsWeaponChanging = false;
        WeaponWasChanged?.Invoke(WeaponPlug);
    }

    public void TryToSetWeapon(WeaponStats weapon)
    {
        IsWeaponChanging = true;
        TryingWeapon = weapon;
        WeaponTryingToChanged?.Invoke();
    }

    public virtual int CalculateTotalDamage()
    {
        int weaponDamage = _currentWeapon != null ? WeaponPlug.CalculateTotalDamage() : 0;
        return Stats.BaseAttackPower + weaponDamage;
    }

    public virtual float CalculateTotalPrepareDelay()
    {
        return Stats.BaseAttackDelay;
    }

    public virtual float CalculateAttackDelay()
    {
        return _currentWeapon != null ? WeaponPlug.CalculateTotalAttackDelay() : 0;
    }

    protected virtual int ReduceDamageByArmor(int damage)
    {
        int totalDamage = damage -= Stats.Armor;

        if(totalDamage < 0)
            totalDamage = 0;

        return totalDamage;
    }

    public virtual bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    protected virtual void Die()
    {
        Debug.Log($"{Stats.Name} is die");
        Died?.Invoke();
    }
}