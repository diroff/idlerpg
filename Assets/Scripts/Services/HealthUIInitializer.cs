using UnityEngine;

public class HealthUIInitializer : MonoBehaviour
{
    [SerializeField] private Fighter _fighter;
    [SerializeField] private UIFighterHealth _uiHealth;

    private void Start()
    {
        if (_fighter == null)
            return;

        _uiHealth.Initialize(_fighter);
    }

    public void Initialize(Fighter fighter)
    {
        _uiHealth.gameObject.SetActive(true);

        _fighter = fighter;
        _uiHealth.Initialize(_fighter);
    }
}