using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    [SerializeField] private SlotMachine _slotMachine;
    [SerializeField] private GameObject _spinBtn;
    [SerializeField] private TextMeshProUGUI _playAmountTMP;
    [SerializeField] private TextMeshProUGUI _balanceTMP;
    [SerializeField] private TextMeshProUGUI _winAmountTitleTMP;
    [SerializeField] private TextMeshProUGUI _winAmountTMP;
    [SerializeField] private GameObject _freeSpinTitle;
    [SerializeField] private TextMeshProUGUI _freeSpinAmountTMP;

    private float _playAmount = 1f;
    private float _balance = 1000f;

    private void Update()
    {
        if (SlotMachine.State == State.CalculatingWins)
        {
            StartCoroutine(CalculateWins());
        }
    }

    public void OnSpinClick()
    {
        // TODO: play spin sound effect
        print($"Spin button clicked!");

        if (SlotMachine.State != State.Ready)
        {
            print($"Cannot spin while state is \"{SlotMachine.State}\"");
            return;
        }

        if (!(_balance >= _playAmount))
        {
            print($"Insufficient balance to spin");
            return;
        }

        _balance -= _playAmount;
        _balanceTMP.SetText($"{_balance:C}");

        StartCoroutine(_slotMachine.SpinReels());
    }

    public void UpdateBalance(float multiplier)
    {
        float winAmount = _playAmount * multiplier;
        _winAmountTMP.SetText($"{winAmount:C}");

        _balance += winAmount;
        _balanceTMP.SetText($"{_balance:C}");
    }

    public void OnIncrementClick()
    {
        print("Increasing play amount");

        switch (_playAmount)
        {
            case 1:
                _playAmount = 5;
                break;
            case 5:
                _playAmount = 10;
                break;
            case 10:
                _playAmount = 100;
                break;
            case 100:
                _playAmount = 1000;
                break;
        }

        _playAmountTMP.SetText($"{_playAmount:C}");
    }

    public void OnDecrementClick()
    {
        print("Increasing play amount");

        switch (_playAmount)
        {
            case 5:
                _playAmount = 1;
                break;
            case 10:
                _playAmount = 5;
                break;
            case 100:
                _playAmount = 10;
                break;
            case 1000:
                _playAmount = 100;
                break;
        }

        _playAmountTMP.SetText($"{_playAmount:C}");
    }

    private IEnumerator CalculateWins()
    {
        List<GameObject> reel1Symbols = SlotUtils.GetChildGameObjects(SlotUtils.GetSymbolBackgrounds(reel: 1));
        List<GameObject> reel2Symbols = SlotUtils.GetChildGameObjects(SlotUtils.GetSymbolBackgrounds(reel: 2));
        List<GameObject> reel3Symbols = SlotUtils.GetChildGameObjects(SlotUtils.GetSymbolBackgrounds(reel: 3));
        List<GameObject> reel4Symbols = SlotUtils.GetChildGameObjects(SlotUtils.GetSymbolBackgrounds(reel: 4));

        List<GameObject> allSymbols = new List<GameObject>(reel1Symbols.Count +
                                                           reel2Symbols.Count +
                                                           reel3Symbols.Count +
                                                           reel4Symbols.Count);

        allSymbols.AddRange(reel1Symbols);
        allSymbols.AddRange(reel2Symbols);
        allSymbols.AddRange(reel3Symbols);
        allSymbols.AddRange(reel4Symbols);

        if (IsMaxWin(allSymbols))
        {
            print("Awarding max win amount!");

            UpdateBalance(multiplier:5000);
        }
        else if (IsSuperBonus(allSymbols))
        {
            const int spinsRemaining = 12;

            print("Super bonus: awarding 12 free spins!");

            _spinBtn.SetActive(false);
            _freeSpinTitle.SetActive(true);
            _freeSpinAmountTMP.gameObject.SetActive(true);
            _winAmountTitleTMP.SetText("Total Win");

            // in case we explicitly entered the bonus from the settings
            if (SlotMachine.WinMode == WinMode.SuperBonus) SlotMachine.WinMode = WinMode.NormalPlay;

            yield return StartCoroutine(RunFreeSpins(spinsRemaining));

            // go back to original selections
            _spinBtn.SetActive(true);
            _freeSpinTitle.SetActive(false);
            _freeSpinAmountTMP.gameObject.SetActive(false);
            _winAmountTitleTMP.SetText("Win");

            if (SlotMachine.WinMode == WinMode.NormalPlay) SlotMachine.WinMode = WinMode.SuperBonus;

        }
        else if (IsBonusSpin(allSymbols))
        {
            const int spinsRemaining = 10;

            print("Normal bonus: awarding 10 free spins!");

            _spinBtn.SetActive(false);
            _freeSpinTitle.SetActive(true);
            _freeSpinAmountTMP.gameObject.SetActive(true);
            _winAmountTitleTMP.SetText("Total Win");

            // in case we explicitly entered the bonus from the settings
            if (SlotMachine.WinMode == WinMode.Bonus) SlotMachine.WinMode = WinMode.NormalPlay;

            yield return StartCoroutine(RunFreeSpins(spinsRemaining));

            // go back to original selections
            _spinBtn.SetActive(true);
            _freeSpinTitle.SetActive(false);
            _freeSpinAmountTMP.gameObject.SetActive(false);
            _winAmountTitleTMP.SetText("Win");

            if (SlotMachine.WinMode == WinMode.NormalPlay) SlotMachine.WinMode = WinMode.Bonus;
        }

        SlotMachine.State = State.Ready;
    }

    private bool IsMaxWin(List<GameObject> allSymbols)
    {
        int maxWinSymbolsCount = 0;

        foreach (GameObject currentSymbol in allSymbols)
        {
            if (currentSymbol.name == "W-symbol(Clone)") maxWinSymbolsCount++;
            if (currentSymbol.name == "C-symbol(Clone)") maxWinSymbolsCount++;
            if (currentSymbol.name == "K-symbol(Clone)") maxWinSymbolsCount++;
            if (currentSymbol.name == "D-symbol(Clone)") maxWinSymbolsCount++;
        }

        if (maxWinSymbolsCount == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsSuperBonus(List<GameObject> allSymbols)
    {
        return HasSymbolCount(allSymbols, Symbol.Frog, requiredCount: 4);
    }

    private bool IsBonusSpin(List<GameObject> allSymbols)
    {
        return HasSymbolCount(allSymbols, Symbol.Frog, requiredCount: 3);
    }

    private IEnumerator RunFreeSpins(int spinsRemaining)
    {
        do
        {
            _freeSpinAmountTMP.SetText(spinsRemaining.ToString());

            yield return StartCoroutine(_slotMachine.SpinReels());

            spinsRemaining--;

        } while (spinsRemaining > 0);
    }

    private bool HasSymbolCount(List<GameObject> allSymbols, Symbol target, int requiredCount)
    {
        int symbolCount = 0;

        foreach (GameObject currentSymbol in allSymbols)
        {
            if (currentSymbol.name == $"{target.ToString()}-symbol(Clone)")
            {
                symbolCount++;
            }
        }

        return symbolCount == requiredCount;
    }
}