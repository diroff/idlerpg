using UnityEngine;

public abstract class ButtonUIInstaller : MonoBehaviour
{
    [SerializeField] private Fight _fight;

    private void OnEnable()
    {
        _fight.FightStarted += OnFightStarted;
        _fight.FightEnded += OnFightFinished;
    }

    private void OnDisable()
    {
        _fight.FightStarted -= OnFightStarted;
        _fight.FightEnded -= OnFightFinished;
    }

    private void Start()
    {
        SetupButtonsState(_fight.FightEnabled);
    }

    private void OnFightStarted()
    {
        SetupButtonsState(true);
    }

    private void OnFightFinished()
    {
        SetupButtonsState(false);
    }

    protected abstract void SetupButtonsState(bool enabled);
}
