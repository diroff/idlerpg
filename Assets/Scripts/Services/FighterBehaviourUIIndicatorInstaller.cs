using UnityEngine;

public class FighterBehaviourUIIndicatorInstaller : FightListener
{
    [SerializeField] private GameObject _uiIndicatorsPanel;
    [SerializeField] private FighterBehaviour _fighter;

    [SerializeField] private GameObject _uiPrepareState;
    [SerializeField] private GameObject _uiAttackState;
    [SerializeField] private GameObject _uiSwitchWeaponState;

    protected override void OnEnable()
    {
        base.OnEnable();
        _fighter.PrepareStateStarted += OnPrepareStateStarted;
        _fighter.AttackStateStarted += OnAttackStateStarted;
        _fighter.SwitchWeaponStateStarted += OnSwitchWeaponStateStarted;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _fighter.PrepareStateStarted -= OnPrepareStateStarted;
        _fighter.AttackStateStarted -= OnAttackStateStarted;
        _fighter.SwitchWeaponStateStarted -= OnSwitchWeaponStateStarted;
    }

    protected override void OnFightStateChangedAction(bool enabled)
    {
        _uiIndicatorsPanel.SetActive(enabled);
    }

    private void OnPrepareStateStarted()
    {
        _uiPrepareState.SetActive(true);

        _uiAttackState.SetActive(false);
        _uiSwitchWeaponState.SetActive(false);
    }

    private void OnAttackStateStarted()
    {
        _uiAttackState.SetActive(true);

        _uiPrepareState.SetActive(false);
        _uiSwitchWeaponState.SetActive(false);
    }

    private void OnSwitchWeaponStateStarted()
    {
        _uiSwitchWeaponState.SetActive(true);
        
        _uiAttackState.SetActive(false);
        _uiPrepareState.SetActive(false);
    }
}