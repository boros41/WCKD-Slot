using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;

    public void OnSettingsClick()
    {
        print("Settings button clicked.");
        _settingsPanel.SetActive(!_settingsPanel.activeSelf);

        // this could be open/active
        GameObject playTable = GameObject.FindGameObjectWithTag(TagManager.PayTable);
        if (playTable != null && playTable.activeSelf)
        {
            playTable.SetActive(false);
        }
    }
}
