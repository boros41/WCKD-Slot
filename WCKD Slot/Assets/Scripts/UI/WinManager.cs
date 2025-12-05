using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    #region Properties
    [SerializeField] private SlotMachine _slotMachine;
    [SerializeField] private GameObject _spinBtn;
    [SerializeField] private TextMeshProUGUI _playAmountTMP;
    [SerializeField] private TextMeshProUGUI _balanceTMP;
    [SerializeField] private TextMeshProUGUI _winAmountTitleTMP;
    [SerializeField] private TextMeshProUGUI _winAmountTMP;
    [SerializeField] private GameObject _freeSpinTitle;
    [SerializeField] private TextMeshProUGUI _freeSpinAmountTMP;
    [SerializeField] private AudioSource _winSoundFx;

    private float _playAmount = 1f;
    private float _balance = 1000f;
    #endregion

    private void Update()
    {
        /*if (SlotMachine.State == State.CalculatingWins)
        {
            StartCoroutine(CalculateWins());
        }*/
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

        _winAmountTMP.SetText("$0.00");
        _balance -= _playAmount;
        _balanceTMP.SetText($"{_balance:C}");

        StartCoroutine(_slotMachine.SpinReels());
    }

    public void UpdateBalance(float multiplier)
    {
        float winAmount = _playAmount * multiplier;
        float oldWinAmount = float.Parse(_winAmountTMP.text[1..]); // in case there are multiple wins at once
        float totalWinAmount = winAmount + oldWinAmount;

        _winAmountTMP.SetText($"{totalWinAmount:C}");

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

    public IEnumerator CalculateWins(List<GameObject> reel1SymbolBgs, List<GameObject> reel2SymbolBgs, List<GameObject> reel3SymbolBgs, List<GameObject> reel4SymbolBgs)
    {
        List<GameObject> reel1Symbols = SlotUtils.GetChildGameObjects(reel1SymbolBgs);
        List<GameObject> reel2Symbols = SlotUtils.GetChildGameObjects(reel2SymbolBgs);
        List<GameObject> reel3Symbols = SlotUtils.GetChildGameObjects(reel3SymbolBgs);
        List<GameObject> reel4Symbols = SlotUtils.GetChildGameObjects(reel4SymbolBgs);

        if (reel4Symbols.Count > 4)
        {
            Debug.LogError("Destroyed symbols detected!");
        }

        List<GameObject> allSymbols = new List<GameObject>(reel1Symbols.Count +
                                                           reel2Symbols.Count +
                                                           reel3Symbols.Count +
                                                           reel4Symbols.Count);

        allSymbols.AddRange(reel1Symbols);
        allSymbols.AddRange(reel2Symbols);
        allSymbols.AddRange(reel3Symbols);
        allSymbols.AddRange(reel4Symbols);

        foreach (GameObject symbol in allSymbols)
        {
            if (symbol == null)
            {
                Debug.LogError("DESTROYED SYMBOL DETECTED!");
            }
        }

        #region Determine Win

        // TODO: For each of these, try to tween the scale of the symbols to highlight the winning symbols for dramatic effect
        if (IsMaxWin(allSymbols))
        {
            print("Awarding max win amount!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, targetSymbols:new List<Symbol>() { Symbol.W , Symbol.C, Symbol.K, Symbol.D});
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier: 5000);
        }

        if (IsSuperBonus(allSymbols))
        {
            print("Super bonus: awarding 12 free spins!");

            const int spinsRemaining = 12;

            // TODO: move to method
            _spinBtn.SetActive(false);
            _freeSpinTitle.SetActive(true);
            _freeSpinAmountTMP.gameObject.SetActive(true);
            _winAmountTitleTMP.SetText("Total Win");

            // in case we explicitly entered the bonus via settings
            if (SlotMachine.WinMode == WinMode.SuperBonus)
                SlotMachine.WinMode = WinMode.NormalPlay;

            yield return StartCoroutine(RunFreeSpins(spinsRemaining));

            // restore original UI
            _spinBtn.SetActive(true);
            _freeSpinTitle.SetActive(false);
            _freeSpinAmountTMP.gameObject.SetActive(false);
            _winAmountTitleTMP.SetText("Win");

            if (SlotMachine.WinMode == WinMode.NormalPlay)
                SlotMachine.WinMode = WinMode.SuperBonus;
        }

        if (IsBonusSpin(allSymbols))
        {
            print("Normal bonus: awarding 10 free spins!");

            const int spinsRemaining = 10;

            _spinBtn.SetActive(false);
            _freeSpinTitle.SetActive(true);
            _freeSpinAmountTMP.gameObject.SetActive(true);
            _winAmountTitleTMP.SetText("Total Win");

            if (SlotMachine.WinMode == WinMode.Bonus)
                SlotMachine.WinMode = WinMode.NormalPlay;

            yield return StartCoroutine(RunFreeSpins(spinsRemaining));

            // restore original UI
            _spinBtn.SetActive(true);
            _freeSpinTitle.SetActive(false);
            _freeSpinAmountTMP.gameObject.SetActive(false);
            _winAmountTitleTMP.SetText("Win");

            if (SlotMachine.WinMode == WinMode.NormalPlay)
                SlotMachine.WinMode = WinMode.Bonus;
        }

        if (IsFourLeafClover(allSymbols))
        {
            const int multiplier = 400;
            print($"Four leaf clover win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Clover);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }

        if (IsFourOf(allSymbols, Symbol.Skull))
        {
            const int multiplier = 50;
            print($"Four skulls win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Skull);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }
        else if (IsThreeOf(allSymbols, Symbol.Skull))
        {
            const int multiplier = 30;
            print($"Three skulls win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Skull);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }

        if (IsFourOf(allSymbols, Symbol.Crocodile))
        {
            const int multiplier = 50;
            print($"Four crocodiles win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Crocodile);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }
        else if (IsThreeOf(allSymbols, Symbol.Crocodile))
        {
            const int multiplier = 30;
            print($"Three crocodiles win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Crocodile);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }

        if (IsFourOf(allSymbols, Symbol.Raven))
        {
            const int multiplier = 20;
            print($"Four ravens win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Raven);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }
        else if (IsThreeOf(allSymbols, Symbol.Raven))
        {
            const int multiplier = 10;
            print($"Three ravens win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Raven);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }

        if (IsFourOf(allSymbols, Symbol.Owl))
        {
            const int multiplier = 3;
            print($"Four owls win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Owl);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }
        else if (IsThreeOf(allSymbols, Symbol.Owl))
        {
            const int multiplier = 2;
            print($"Three owls win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Owl);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }

        if (IsFourOf(allSymbols, Symbol.Bat))
        {
            const int multiplier = 5;
            print($"Four bats win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Bat);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }
        else if (IsThreeOf(allSymbols, Symbol.Bat))
        {
            const int multiplier = 4;
            print($"Three bats win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Bat);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }

        if (IsFourOf(allSymbols, Symbol.Moth))
        {
            const int multiplier = 3;
            print($"Four moths win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Moth);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }
        else if (IsThreeOf(allSymbols, Symbol.Moth))
        {
            const int multiplier = 2;
            print($"Three moths win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Moth);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }

        if (IsFourOf(allSymbols, Symbol.Spider))
        {
            const int multiplier = 3;
            print($"Four spiders win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Spider);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }
        else if (IsThreeOf(allSymbols, Symbol.Spider))
        {
            const int multiplier = 2;
            print($"Three spiders win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Spider);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }

        if (IsFourOf(allSymbols, Symbol.Rose))
        {
            const int multiplier = 3;
            print($"Four roses win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Rose);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }
        else if (IsThreeOf(allSymbols, Symbol.Rose))
        {
            const int multiplier = 2;
            print($"Three roses win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Rose);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }

        if (IsFourOf(allSymbols, Symbol.LilyPad))
        {
            const int multiplier = 2;
            print($"Four lily pads win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.LilyPad);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }
        else if (IsThreeOf(allSymbols, Symbol.LilyPad))
        {
            const int multiplier = 1;
            print($"Three lily pads win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.LilyPad);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }

        if (IsFourOf(allSymbols, Symbol.Flower))
        {
            const int multiplier = 2;
            print($"Four flowers win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Flower);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }
        else if (IsThreeOf(allSymbols, Symbol.Flower))
        {
            const int multiplier = 1;
            print($"Three flowers win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Flower);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }

        if (IsFourOf(allSymbols, Symbol.DeadTree))
        {
            const int multiplier = 2;
            print($"Four dead trees win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.DeadTree);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }
        else if (IsThreeOf(allSymbols, Symbol.DeadTree))
        {
            const int multiplier = 1;
            print($"Three dead trees win, awarding {multiplier}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.DeadTree);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

            UpdateBalance(multiplier);
        }

        #endregion

        SlotMachine.State = State.Ready;
        print($"Current state: {SlotMachine.State}");
    }

    private IEnumerator AnimateSymbols(List<GameObject> symbols)
    {
        bool isFinished = false;
        GameObject lastSymbol = symbols[^1];

        _winSoundFx.Play();

        foreach (GameObject currentSymbol in symbols)
        {
            LeanTween.scale(currentSymbol, new Vector3(1.2f, 1.2f, 1f), 0.5f)
                     .setOnComplete(() =>
                     {
                         if (currentSymbol != lastSymbol)
                         {
                             LeanTween.scale(currentSymbol, Vector3.one, 0.5f);
                         }
                         else
                         {
                             LeanTween.scale(currentSymbol, Vector3.one, 0.5f)
                                      .setOnComplete(() => isFinished = true);
                         }
                     });

            /*if (currentSymbol != lastSymbol)
            {
                LeanTween.scale(currentSymbol, new Vector3(1.2f, 1.2f, 1f), 0.5f)
                         .setOnComplete(() =>
                         {
                             LeanTween.scale(currentSymbol, Vector3.one, 0.5f);
                         });
            }
            else
            {
                LeanTween.scale(currentSymbol, new Vector3(1.2f, 1.2f, 1f), 0.5f)
                         .setOnComplete(() =>
                         {
                             LeanTween.scale(currentSymbol, Vector3.one, 0.5f)
                                      .setOnComplete(() => isFinished = true);
                         });
            }*/
        }

        yield return new WaitUntil(() => isFinished);
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
    private bool IsFourLeafClover(List<GameObject> allSymbols)
    {
        return HasSymbolCount(allSymbols, Symbol.Clover, 4);
    }

    private bool IsFourOf(List<GameObject> allSymbols, Symbol symbol)
    {
        return HasSymbolCount(allSymbols, symbol, requiredCount: 4);
    }

    private bool IsThreeOf(List<GameObject> allSymbols, Symbol symbol)
    {
        return HasSymbolCount(allSymbols, symbol, requiredCount: 3);
    }

    private bool HasSymbolCount(List<GameObject> allSymbols, Symbol targetSymbol, int requiredCount)
    {
        if (allSymbols.Count > 16)
        {
            Debug.LogError("Destroyed symbol detected!");
        }

        int symbolCount = 0;

        foreach (GameObject currentSymbol in allSymbols)
        {
            if (currentSymbol == null) continue; // skip if we destroyed the object, only happens at the end of bonus spins for some reason

            if (currentSymbol.name == $"{targetSymbol.ToString()}-symbol(Clone)")
            {
                symbolCount++;
            }
        }

        if (symbolCount == requiredCount)
        {
            return true;
        }
        else if (symbolCount >= requiredCount)
        {
            // TODO: Handle user winning more than the required amount by substituting extra symbols with a multiplier or something 
            return true;
        }
        else
        {
            return false;
        }
    }

    private List<GameObject> GetTargetSymbols(List<GameObject> allSymbols, Symbol targetSymbol)
    {
        List<GameObject> symbols = new List<GameObject>();

        foreach (GameObject currentSymbol in allSymbols)
        {
            if (currentSymbol == null)
            {
                continue;
            }; // skip if we destroyed the object, only happens at the end of bonus spins for some reason

            if (currentSymbol.name == $"{targetSymbol.ToString()}-symbol(Clone)")
            {
                symbols.Add(currentSymbol);
            }
        }

        return symbols;
    }

    private List<GameObject> GetTargetSymbols(List<GameObject> allSymbols, List<Symbol> targetSymbols)
    {
        List<GameObject> symbols = new List<GameObject>();

        foreach (GameObject currentSymbol in allSymbols)
        {
            if (currentSymbol == null)
            {
                continue;
            } // safeguard in case we destroyed it

            foreach (Symbol targetSymbol in targetSymbols)
            {
                if (currentSymbol.name == $"{targetSymbol.ToString()}-symbol(Clone)")
                {
                    symbols.Add(currentSymbol);
                }
            }
        }

        return symbols;
    }
}