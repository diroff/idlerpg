using UnityEngine;
using UnityEngine.UI;

public class FightButtonUIInstaller : FightListener
{
    [SerializeField] private Button _startFightButton;
    [SerializeField] private Button _leaveButton;

    protected override void OnFightStateChangedAction(bool enabled)
    {
        _leaveButton.gameObject.SetActive(enabled);
        _startFightButton.gameObject.SetActive(!enabled);
    }
}