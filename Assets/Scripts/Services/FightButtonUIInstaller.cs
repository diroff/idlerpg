using UnityEngine;
using UnityEngine.UI;

public class FightButtonUIInstaller : MonoBehaviour
{
    [SerializeField] private FightInitializer _fightCreator;

    [SerializeField] private Button _button;

    public void ChangeFightState()
    {
        _fightCreator.ChangeFightState();
    }
}