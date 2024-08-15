using UnityEngine;
using UnityEngine.UI;

public class FightButtonUIInstaller : MonoBehaviour
{
    [SerializeField] private Fight _fight;

    [SerializeField] private Button _startFightButton;
    [SerializeField] private Button _leaveFightButton;

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

    private void SetupButtonsState(bool enabled)
    {
        bool isFight = enabled;

        _startFightButton.gameObject.SetActive(!isFight);
        _leaveFightButton.gameObject.SetActive(isFight);
    }
}