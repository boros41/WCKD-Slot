using System;
using TMPro;
using UnityEngine;

public class WinsDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;

    public void OnChange()
    {
        int selectedIndex = _dropdown.value;
        string selection = _dropdown.options[selectedIndex].text;

        print($"Wins dropdown selection: {selection}");

        switch (selection)
        {
            case "Normal Play":
                SlotMachine.WinMode = WinMode.NormalPlay;
                break;
            case "Max Win":
                SlotMachine.WinMode = WinMode.MaxWin;
                break;
            case "Bonus":
                SlotMachine.WinMode = WinMode.Bonus;
                break;
            default:
                throw new ArgumentException("No valid win mode specified");
        }
    }
}
