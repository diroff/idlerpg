using UnityEngine;

public abstract class FightListener : MonoBehaviour
{
    [SerializeField] private Fight _fight;

    protected virtual void OnEnable()
    {
        _fight.FightStarted += OnFightStarted;
        _fight.FightEnded += OnFightFinished;
    }

    protected virtual void OnDisable()
    {
        _fight.FightStarted -= OnFightStarted;
        _fight.FightEnded -= OnFightFinished;
    }

    private void Start()
    {
        OnFightStateChangedAction(_fight.FightEnabled);
    }

    private void OnFightStarted()
    {
        OnFightStateChangedAction(true);
    }

    private void OnFightFinished()
    {
        OnFightStateChangedAction(false);
    }

    protected abstract void OnFightStateChangedAction(bool enabled);
}
