using UnityEngine;
using UnityEngine.Events;

public abstract class Fighter : MonoBehaviour, IDamageable
{
    [SerializeField] protected CharacterStats CharacterStats;

    protected int CurrentHealth;

    public UnityAction<int> HealthChanged;
    public UnityAction Died;

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            Debug.LogError("Damage < 0");

        CurrentHealth -= damage;

        if(CurrentHealth < 0)
            CurrentHealth = 0;

        HealthChanged?.Invoke(CurrentHealth);

        if (IsDead())
            Die();
    }

    protected virtual bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    protected virtual void Die()
    {
        Died?.Invoke();
    }
}