using UnityEngine;

public class HealthUIInitializer : MonoBehaviour
{
    [SerializeField] private Fighter _fighter;
    [SerializeField] private UIFighterHealth _uiHealth;

    public void Initialize()
    {
        _uiHealth.gameObject.SetActive(true);
        _uiHealth.Initialize(_fighter);
    }
}