using TMPro;
using UnityEngine;

public class PayTablePopup : MonoBehaviour
{
    [SerializeField] private GameObject _payTable;

    public void OnPayTableClick()
    {
        _payTable.SetActive(!_payTable.activeSelf);
    }

    public void OnPayTableClose()
    {
        _payTable.SetActive(false);

        // this could be open/active
        GameObject settingsPanel = GameObject.FindGameObjectWithTag(TagManager.SettingsPanel);
        if (settingsPanel != null && settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
        }
    }
}
