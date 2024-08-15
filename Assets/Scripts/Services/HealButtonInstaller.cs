using UnityEngine;
using UnityEngine.UI;

public class HealButtonInstaller : FightListener
{
    [SerializeField] private Button _healButton;

    protected override void OnFightStateChangedAction(bool enabled)
    {
        _healButton.gameObject.SetActive(!enabled);
    }
}