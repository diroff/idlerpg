using UnityEngine;
using UnityEngine.UI;

public class HealButtonInstaller : ButtonUIInstaller
{
    [SerializeField] private Button _healButton;

    protected override void SetupButtonsState(bool enabled)
    {
        _healButton.gameObject.SetActive(!enabled);
    }
}