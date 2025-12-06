using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;

    public void OnSettingsClick()
    {
        print("Settings button clicked.");
        _settingsPanel.SetActive(!_settingsPanel.activeSelf);
    }
}
