using UnityEngine;
using UnityEngine.UI;

public class FightButtonUIInstaller : ButtonUIInstaller
{
    [SerializeField] private Button _startFightButton;
    [SerializeField] private Button _leaveButton;

    protected override void SetupButtonsState(bool enabled)
    {
        _leaveButton.gameObject.SetActive(enabled);
        _startFightButton.gameObject.SetActive(!enabled);
    }
}