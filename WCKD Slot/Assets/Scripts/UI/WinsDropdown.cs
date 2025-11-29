using System;
using System.Collections.Generic;
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
            case "Super Bonus":
                SlotMachine.WinMode = WinMode.SuperBonus;
                break;
            case "Bonus":
                SlotMachine.WinMode = WinMode.Bonus;
                break;
            case "Four Leaf Clover":
                SlotMachine.WinMode = WinMode.FourLeafClover;
                break;
            case "Four Skulls":
                SlotMachine.WinMode = WinMode.FourSkulls;
                break;
            case "Three Skulls":
                SlotMachine.WinMode = WinMode.ThreeSkulls;
                break;
            case "Four Crocodiles":
                SlotMachine.WinMode = WinMode.FourCrocodiles;
                break;
            case "Three Crocodiles":
                SlotMachine.WinMode = WinMode.ThreeCrocodiles;
                break;
            case "Four Ravens":
                SlotMachine.WinMode = WinMode.FourRavens;
                break;
            case "Three Ravens":
                SlotMachine.WinMode = WinMode.ThreeRavens;
                break;
            case "Four Owls":
                SlotMachine.WinMode = WinMode.FourOwls;
                break;
            case "Three Owls":
                SlotMachine.WinMode = WinMode.ThreeOwls;
                break;
            case "Four Bats":
                SlotMachine.WinMode = WinMode.FourBats;
                break;
            case "Three Bats":
                SlotMachine.WinMode = WinMode.ThreeBats;
                break;
            case "Four Moths":
                SlotMachine.WinMode = WinMode.FourMoths;
                break;
            case "Three Moths":
                SlotMachine.WinMode = WinMode.ThreeMoths;
                break;
            case "Four Spiders":
                SlotMachine.WinMode = WinMode.FourSpiders;
                break;
            case "Three Spiders":
                SlotMachine.WinMode = WinMode.ThreeSpiders;
                break;
            case "Four Roses":
                SlotMachine.WinMode = WinMode.FourRoses;
                break;
            case "Three Roses":
                SlotMachine.WinMode = WinMode.ThreeRoses;
                break;
            case "Four Lily Pads":
                SlotMachine.WinMode = WinMode.FourLilyPads;
                break;
            case "Three Lily Pads":
                SlotMachine.WinMode = WinMode.ThreeLilyPads;
                break;
            case "Four Flowers":
                SlotMachine.WinMode = WinMode.FourFlowers;
                break;
            case "Three Flowers":
                SlotMachine.WinMode = WinMode.ThreeFlowers;
                break;
            case "Four Dead Trees":
                SlotMachine.WinMode = WinMode.FourDeadTrees;
                break;
            case "Three Dead Trees":
                SlotMachine.WinMode = WinMode.ThreeDeadTrees;
                break;
            default:
                throw new ArgumentException("No valid win mode specified");
        }
    }
}
