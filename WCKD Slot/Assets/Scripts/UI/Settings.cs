using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    private bool _isActive;

    public void OnSettingsClick()
    {
        _isActive = !_isActive;
        _settingsPanel.SetActive(_isActive);
    }
}
