using UnityEngine;

public class FPSInstaller : MonoBehaviour
{
    [SerializeField] private int _targetFPS = 240;

    private void Awake()
    {
        Application.targetFrameRate = _targetFPS;
    }
}