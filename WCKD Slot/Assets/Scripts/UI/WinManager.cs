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

    private static float _popupWinTextCachedY;
    #endregion

    public void OnSpinClick()
    {
        // TODO: play spin sound effect
        print($"Spin button clicked!");

        // this could be open/active
        GameObject settingsPanel = GameObject.FindGameObjectWithTag(TagManager.SettingsPanel);
        if (settingsPanel != null && settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
        }

        GameObject payTable = GameObject.FindGameObjectWithTag(TagManager.PayTable);
        if (payTable != null && payTable.activeSelf)
        {
            payTable.SetActive(false);
        }

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

    public void UpdateBalance(int multiplier)
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
            print($"Awarding max win amount! {(int)Multiplier.WCKD}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, targetSymbols:new List<Symbol>() { Symbol.W , Symbol.C, Symbol.K, Symbol.D});
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.WCKD));
        }

        if (IsSuperBonus(allSymbols))
        {
            print("Super bonus: awarding 12 free spins!");

            const int spinsRemaining = 12;

            // TODO: move before and after to method
            _spinBtn.SetActive(false);
            _freeSpinTitle.SetActive(true);
            _freeSpinAmountTMP.gameObject.SetActive(true);
            _winAmountTitleTMP.SetText("Total Win");

            // in case we explicitly entered the bonus via settings
            if (SlotMachine.WinMode == WinMode.SuperBonus)
                SlotMachine.WinMode = WinMode.NormalPlay;

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Frog);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

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

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Frog);
            yield return StartCoroutine(AnimateSymbols(targetSymbols));

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
            print($"Four leaf clover win, awarding {(int)Multiplier.FourLeafClover}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Clover);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.FourLeafClover));
        }

        if (IsFourOf(allSymbols, Symbol.Skull))
        {
            print($"Four skulls win, awarding {(int)Multiplier.SkullFour}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Skull);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.SkullFour));
        }
        else if (IsThreeOf(allSymbols, Symbol.Skull))
        {
            print($"Three skulls win, awarding {(int)Multiplier.SkullThree}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Skull);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.SkullThree));
        }

        if (IsFourOf(allSymbols, Symbol.Crocodile))
        {
            print($"Four crocodiles win, awarding {(int)Multiplier.CrocodileFour}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Crocodile);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.CrocodileFour));
        }
        else if (IsThreeOf(allSymbols, Symbol.Crocodile))
        {
            print($"Three crocodiles win, awarding {(int)Multiplier.CrocodileThree}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Crocodile);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.CrocodileThree));
        }

        if (IsFourOf(allSymbols, Symbol.Raven))
        {
            print($"Four ravens win, awarding {(int)Multiplier.RavenFour}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Raven);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.RavenFour));
        }
        else if (IsThreeOf(allSymbols, Symbol.Raven))
        {
            print($"Three ravens win, awarding {(int)Multiplier.RavenThree}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Raven);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.RavenThree));
        }

        if (IsFourOf(allSymbols, Symbol.Owl))
        {
            print($"Four owls win, awarding {(int)Multiplier.OwlFour}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Owl);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.OwlFour));
        }
        else if (IsThreeOf(allSymbols, Symbol.Owl))
        {
            print($"Three owls win, awarding {(int)Multiplier.OwlThree}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Owl);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.OwlThree));
        }

        if (IsFourOf(allSymbols, Symbol.Bat))
        {
            print($"Four bats win, awarding {(int)Multiplier.BatFour}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Bat);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.BatFour));
        }
        else if (IsThreeOf(allSymbols, Symbol.Bat))
        {
            print($"Three bats win, awarding {(int)Multiplier.BatThree}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Bat);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.BatThree));
        }

        if (IsFourOf(allSymbols, Symbol.Moth))
        {
            print($"Four moths win, awarding {(int)Multiplier.MothFour}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Moth);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.MothFour));
        }
        else if (IsThreeOf(allSymbols, Symbol.Moth))
        {
            print($"Three moths win, awarding {(int)Multiplier.MothThree}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Moth);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.MothThree));
        }

        if (IsFourOf(allSymbols, Symbol.Spider))
        {
            print($"Four spiders win, awarding {(int)Multiplier.SpiderFour}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Spider);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.SpiderFour));
        }
        else if (IsThreeOf(allSymbols, Symbol.Spider))
        {
            print($"Three spiders win, awarding {(int)Multiplier.SpiderThree}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Spider);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.SpiderThree));
        }

        if (IsFourOf(allSymbols, Symbol.Rose))
        {
            print($"Four roses win, awarding {(int)Multiplier.RoseFour}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Rose);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.RoseFour));
        }
        else if (IsThreeOf(allSymbols, Symbol.Rose))
        {
            print($"Three roses win, awarding {(int)Multiplier.RoseThree}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Rose);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.RoseThree));
        }

        if (IsFourOf(allSymbols, Symbol.LilyPad))
        {
            print($"Four lily pads win, awarding {(int)Multiplier.LilyPadFour}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.LilyPad);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.LilyPadFour));
        }
        else if (IsThreeOf(allSymbols, Symbol.LilyPad))
        {
            print($"Three lily pads win, awarding {(int)Multiplier.LilyPadThree}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.LilyPad);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.LilyPadThree));
        }

        if (IsFourOf(allSymbols, Symbol.Flower))
        {
            print($"Four flowers win, awarding {(int)Multiplier.FlowerFour}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Flower);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.FlowerFour));
        }
        else if (IsThreeOf(allSymbols, Symbol.Flower))
        {
            print($"Three flowers win, awarding {(int)Multiplier.FlowerThree}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.Flower);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.FlowerThree));
        }

        if (IsFourOf(allSymbols, Symbol.DeadTree))
        {
            print($"Four dead trees win, awarding {(int)Multiplier.DeadTreeFour}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.DeadTree);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.DeadTreeFour));
        }
        else if (IsThreeOf(allSymbols, Symbol.DeadTree))
        {
            print($"Three dead trees win, awarding {(int)Multiplier.DeadTreeThree}x!");

            List<GameObject> targetSymbols = GetTargetSymbols(allSymbols, Symbol.DeadTree);
            yield return StartCoroutine(AnimateSymbols(targetSymbols, (int)Multiplier.DeadTreeThree));
        }

        #endregion

        _popupWinTextCachedY = 0; // all wins finished, reset
        SlotMachine.State = State.Ready;
        print($"Current state: {SlotMachine.State}");
    }

    // symbols: winning number of a particular set of symbols i.e., four clovers
    private IEnumerator AnimateSymbols(List<GameObject> symbols, int multiplier)
    {
        bool isFinished = false;
        GameObject lastSymbol = symbols[^1];

        _winSoundFx.Play();

        DisplayPerSymbolWin(symbols, multiplier);

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
        }

        UpdateBalance(multiplier);
        DisplayTotalWin(multiplier);

        yield return new WaitUntil(() => isFinished);
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
        }

        yield return new WaitUntil(() => isFinished);
    }

    private void DisplayPerSymbolWin(List<GameObject> symbols, int multiplier)
    {
        if (!symbols.Any())
        {
            print("Unable to animate popup win text, symbols list was empty");
            return;
        }

        float perSymbolContribution = (multiplier * _playAmount) / symbols.Count;
        const string path = "Prefabs/WinTexts/PopupWinText";
        GameObject popupWinTextPrefab = Resources.Load<GameObject>(path);

        foreach (Transform winTextCanvas in popupWinTextPrefab.transform)
        {
            foreach (Transform winTextTransform in winTextCanvas.gameObject.transform)
            {
                TextMeshProUGUI winText = winTextTransform.gameObject.GetComponent<TextMeshProUGUI>();
                winText.text = $"{perSymbolContribution:C}";
            }
        }

        foreach (GameObject currentSymbol in symbols)
        {
            // since this will be a child to currentSymbol, it will in turn be tween scaled in AnimateSymbols()
            GameObject popupWinText = Instantiate(popupWinTextPrefab, parent:currentSymbol.transform);
            float moveTo = popupWinText.transform.position.y + 0.3f;

            LeanTween.moveY(popupWinText, moveTo, 1f)
                     .setOnComplete(() => Destroy(popupWinText));
        }
    }

    private void DisplayTotalWin(int multiplier)
    {
        float totalWin = multiplier * _playAmount;
        const string path = "Prefabs/WinTexts/PopupWinText";
        GameObject popupWinTextPrefab = Resources.Load<GameObject>(path);

        foreach (Transform winTextCanvas in popupWinTextPrefab.transform)
        {
            foreach (Transform winTextTransform in winTextCanvas.gameObject.transform)
            {
                TextMeshProUGUI winText = winTextTransform.gameObject.GetComponent<TextMeshProUGUI>();
                winText.text = $"{totalWin:C}";
            }
        }

        GameObject popupWinText = Instantiate(popupWinTextPrefab, new Vector2(0, _popupWinTextCachedY), Quaternion.identity);
        float moveTo = _popupWinTextCachedY + 0.3f;
        _popupWinTextCachedY = moveTo;

        LeanTween.moveY(popupWinText, moveTo, 1f)
                 .setOnComplete(() => Destroy(popupWinText));
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