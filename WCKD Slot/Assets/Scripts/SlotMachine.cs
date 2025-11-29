using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    private Reel _reel1;
    private Reel _reel2;
    private Reel _reel3;
    private Reel _reel4;
    public static State State;
    public static WinMode WinMode = WinMode.NormalPlay;

    [CanBeNull] public event Action<float> MaxWinEvent;

    [SerializeField] private float _spinDuration = 2.5f;

    private void Start()
    {
        State = State.Incomplete;

        _reel1 = new Reel(new ReadOnlyCollection<Symbol>(
            new List<Symbol>()
            {
                Symbol.LilyPad, Symbol.Flower, Symbol.DeadTree, Symbol.Owl, Symbol.LilyPad, Symbol.Bat, Symbol.Flower, Symbol.Skull, Symbol.Flower, Symbol.DeadTree, Symbol.Moth, Symbol.Frog, Symbol.Flower, Symbol.Crocodile, Symbol.LilyPad, Symbol.Owl, Symbol.DeadTree, Symbol.Raven, Symbol.LilyPad, Symbol.Rose, Symbol.Flower, Symbol.DeadTree, Symbol.Spider, Symbol.DeadTree, Symbol.LilyPad, Symbol.Skull, Symbol.Flower, Symbol.Bat, Symbol.Flower, Symbol.Spider, Symbol.Crocodile, Symbol.Clover, Symbol.Flower, Symbol.Owl, Symbol.Flower, Symbol.LilyPad, Symbol.Raven, Symbol.DeadTree, Symbol.Flower, Symbol.Rose, Symbol.Flower, Symbol.LilyPad, Symbol.Moth, Symbol.DeadTree, Symbol.DeadTree, Symbol.Skull, Symbol.Owl, Symbol.W, Symbol.LilyPad, Symbol.Bat, Symbol.DeadTree, Symbol.LilyPad, Symbol.Crocodile, Symbol.Rose, Symbol.LilyPad, Symbol.Bat, Symbol.Rose, Symbol.LilyPad, Symbol.Spider, Symbol.Moth, Symbol.LilyPad, Symbol.Raven, Symbol.Spider, Symbol.DeadTree
            }
        ));

        _reel2 = new Reel(new ReadOnlyCollection<Symbol>(
            new List<Symbol>()
            {
                Symbol.LilyPad, Symbol.Flower, Symbol.DeadTree, Symbol.Owl, Symbol.LilyPad, Symbol.Bat, Symbol.Flower, Symbol.Skull, Symbol.Flower, Symbol.DeadTree, Symbol.Moth, Symbol.LilyPad, Symbol.Crocodile, Symbol.Flower, Symbol.LilyPad, Symbol.Spider, Symbol.DeadTree, Symbol.Raven, Symbol.LilyPad, Symbol.Rose, Symbol.Flower, Symbol.DeadTree, Symbol.Owl, Symbol.LilyPad, Symbol.Skull, Symbol.Flower, Symbol.Bat, Symbol.LilyPad, Symbol.Spider, Symbol.Crocodile, Symbol.Clover, Symbol.Flower, Symbol.Owl, Symbol.LilyPad, Symbol.Flower, Symbol.Raven, Symbol.DeadTree, Symbol.LilyPad, Symbol.Rose, Symbol.Flower, Symbol.LilyPad, Symbol.Moth, Symbol.DeadTree, Symbol.DeadTree, Symbol.Skull, Symbol.Owl, Symbol.C, Symbol.LilyPad, Symbol.Bat, Symbol.DeadTree, Symbol.LilyPad, Symbol.Crocodile, Symbol.Rose, Symbol.LilyPad, Symbol.Bat, Symbol.Rose, Symbol.Flower, Symbol.LilyPad, Symbol.Spider, Symbol.Moth, Symbol.Flower, Symbol.Raven, Symbol.Flower, Symbol.LilyPad, Symbol.Owl, Symbol.DeadTree, Symbol.Spider, Symbol.Flower, Symbol.LilyPad, Symbol.Skull, Symbol.Flower, Symbol.Bat, Symbol.DeadTree, Symbol.LilyPad, Symbol.Rose, Symbol.Flower, Symbol.Moth, Symbol.DeadTree, Symbol.Spider, Symbol.Flower, Symbol.LilyPad, Symbol.DeadTree, Symbol.Frog, Symbol.DeadTree, Symbol.DeadTree, Symbol.DeadTree
            }
        ));

        _reel3 = new Reel(new ReadOnlyCollection<Symbol>(
            new List<Symbol>()
            {
                Symbol.LilyPad, Symbol.Flower, Symbol.DeadTree, Symbol.Bat, Symbol.LilyPad, Symbol.Owl, Symbol.Flower, Symbol.Skull, Symbol.Flower, Symbol.DeadTree, Symbol.Moth, Symbol.LilyPad, Symbol.Crocodile, Symbol.Flower, Symbol.LilyPad, Symbol.Spider, Symbol.DeadTree, Symbol.Raven, Symbol.LilyPad, Symbol.Rose, Symbol.Flower, Symbol.DeadTree, Symbol.Owl, Symbol.LilyPad, Symbol.Skull, Symbol.Flower, Symbol.Bat, Symbol.LilyPad, Symbol.Spider, Symbol.Crocodile, Symbol.Clover, Symbol.Flower, Symbol.Owl, Symbol.LilyPad, Symbol.Flower, Symbol.Raven, Symbol.DeadTree, Symbol.LilyPad, Symbol.Rose, Symbol.Flower, Symbol.LilyPad, Symbol.Moth, Symbol.DeadTree, Symbol.DeadTree, Symbol.Skull, Symbol.Owl, Symbol.K, Symbol.LilyPad, Symbol.Bat, Symbol.DeadTree, Symbol.LilyPad, Symbol.Crocodile, Symbol.Rose, Symbol.LilyPad, Symbol.Bat, Symbol.Rose, Symbol.Flower, Symbol.LilyPad, Symbol.Spider, Symbol.Moth, Symbol.Flower, Symbol.Raven, Symbol.Flower, Symbol.LilyPad, Symbol.Owl, Symbol.DeadTree, Symbol.Spider, Symbol.Flower, Symbol.LilyPad, Symbol.Skull, Symbol.Flower, Symbol.Owl, Symbol.DeadTree, Symbol.LilyPad, Symbol.Rose, Symbol.Flower, Symbol.Moth, Symbol.DeadTree, Symbol.Spider, Symbol.Flower, Symbol.DeadTree, Symbol.Frog, Symbol.DeadTree, Symbol.DeadTree, Symbol.DeadTree, Symbol.DeadTree
            }
        ));

        _reel4 = new Reel(new ReadOnlyCollection<Symbol>(
            new List<Symbol>()
            {
                Symbol.LilyPad, Symbol.Flower, Symbol.DeadTree, Symbol.Bat, Symbol.LilyPad, Symbol.Owl, Symbol.Flower, Symbol.Skull, Symbol.Flower, Symbol.DeadTree, Symbol.Moth, Symbol.LilyPad, Symbol.Crocodile, Symbol.Flower, Symbol.LilyPad, Symbol.Spider, Symbol.DeadTree, Symbol.Raven, Symbol.LilyPad, Symbol.Rose, Symbol.Flower, Symbol.DeadTree, Symbol.Owl, Symbol.LilyPad, Symbol.Skull, Symbol.Flower, Symbol.Bat, Symbol.LilyPad, Symbol.Spider, Symbol.Crocodile, Symbol.Clover, Symbol.Flower, Symbol.Owl, Symbol.LilyPad, Symbol.Flower, Symbol.Raven, Symbol.DeadTree, Symbol.LilyPad, Symbol.Rose, Symbol.Flower, Symbol.LilyPad, Symbol.Moth, Symbol.DeadTree, Symbol.DeadTree, Symbol.Skull, Symbol.Owl, Symbol.D, Symbol.LilyPad, Symbol.Bat, Symbol.DeadTree, Symbol.LilyPad, Symbol.Crocodile, Symbol.Rose, Symbol.LilyPad, Symbol.Bat, Symbol.Rose, Symbol.Flower, Symbol.LilyPad, Symbol.Spider, Symbol.Moth, Symbol.Flower, Symbol.Raven, Symbol.Flower, Symbol.LilyPad, Symbol.Owl, Symbol.DeadTree, Symbol.Spider, Symbol.Flower, Symbol.LilyPad, Symbol.Skull, Symbol.Flower, Symbol.Bat, Symbol.DeadTree, Symbol.LilyPad, Symbol.Rose, Symbol.Flower, Symbol.Moth, Symbol.DeadTree, Symbol.Spider, Symbol.Flower, Symbol.LilyPad, Symbol.DeadTree, Symbol.Frog, Symbol.DeadTree, Symbol.DeadTree, Symbol.DeadTree, Symbol.LilyPad, Symbol.Crocodile, Symbol.Flower, Symbol.LilyPad, Symbol.Raven, Symbol.LilyPad, Symbol.Skull, Symbol.LilyPad, Symbol.Bat, Symbol.Owl, Symbol.Spider, Symbol.Rose, Symbol.Moth, Symbol.Flower
            }
        ));

        _reel1.PrintSymbols(1);
        _reel2.PrintSymbols(2);
        _reel3.PrintSymbols(3);
        _reel4.PrintSymbols(4);

        PopulateReels(Reel.Row1Y, Reel.Row2Y, Reel.Row3Y, Reel.Row4Y);

        State = State.Ready;
    }

    private void Update()
    {
        if (State == State.CalculatingWins)
        {
            CalculateWins();
        }
    }

    #region OnGameLaunch

    private List<Dictionary<int, Symbol>> DetermineSymbols()
    {
        List<Reel> reels = new List<Reel>() { _reel1, _reel2, _reel3, _reel4 };
        List<Dictionary<int, Symbol>> determinedSymbols = new List<Dictionary<int, Symbol>>(4) { new(4), new(4), new(4), new(4) };
        List<List<int>> stopIndices = new List<List<int>>(4);

        switch (WinMode)
        {
            case WinMode.NormalPlay:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(), _reel2.StopIndices(), _reel3.StopIndices(), _reel4.StopIndices()
                };
                break;
            case WinMode.MaxWin: // W,C,K,D symbols
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.W), _reel2.StopIndices(Symbol.C), _reel3.StopIndices(Symbol.K), _reel4.StopIndices(Symbol.D)
                };
                break;
            case WinMode.Bonus: // three frogs
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Frog), _reel2.StopIndices(Symbol.Frog), _reel3.StopIndices(Symbol.Frog), _reel4.StopIndices()
                };
                break;
            case WinMode.SuperBonus: // four frogs
                throw new NotImplementedException("Super bonus feature not implemented");
        }

        // store determined symbols of each reel with their index (indexed from Reel.Strip list)
        for (int reel = 0; reel < determinedSymbols.Count; reel++)
        {
            foreach (int i in stopIndices[reel])
            {
                determinedSymbols[reel].Add(i, reels[reel].Strip[i]);
            }
        }

        PrintDeterminedSymbols(determinedSymbols);

        return determinedSymbols;

        //PopulateReels(determinedSymbols, row1Y, row2Y, row3Y, row4Y);
    }

    // this instantiates the symbols when the player first loads the game
    public void PopulateReels(float row1Y, float row2Y, float row3Y, float row4Y)
    {
        List<Dictionary<int, Symbol>> determinedSymbols = DetermineSymbols();
        const string symbolPath = "Prefabs/Symbols/";

        for (int reel = 0; reel < 4; reel++)
        {
            GameObject parent = null;
            switch (reel)
            {
                // TODO: Move each case into a separate method for populating that reel. Look into delegate methods to reduce duplication.
                case 0: // reel 1
                    parent = GameObject.FindGameObjectWithTag(TagManager.Reel1);
                    List<GameObject> symbolBackgrounds1 = new List<GameObject>(4);

                    for (int i = 0; i < symbolBackgrounds1.Capacity; i++)
                    {
                        GameObject symbolBackground = Resources.Load<GameObject>($"{symbolPath}symbol-background");

                        symbolBackgrounds1.Add(Instantiate(symbolBackground, parent.transform));
                    }

                    symbolBackgrounds1[0].transform.localPosition = new Vector3(Reel.Reel1X, row1Y, 1f);
                    symbolBackgrounds1[1].transform.localPosition = new Vector3(Reel.Reel1X, row2Y, 1f);
                    symbolBackgrounds1[2].transform.localPosition = new Vector3(Reel.Reel1X, row3Y, 1f);
                    symbolBackgrounds1[3].transform.localPosition = new Vector3(Reel.Reel1X, row4Y, 1f);

                    int j = 0;
                    foreach (KeyValuePair<int, Symbol> kvp in determinedSymbols[reel])
                    {
                        GameObject symbol = Resources.Load<GameObject>($"{symbolPath}{kvp.Value}-symbol");
                        Instantiate(symbol, symbolBackgrounds1[j].transform);

                        j++;
                    }

                    break;
                case 1: // reel 2
                    parent = GameObject.FindGameObjectWithTag(TagManager.Reel2);
                    List<GameObject> symbolBackgrounds2 = new List<GameObject>(4);

                    for (int i = 0; i < symbolBackgrounds2.Capacity; i++)
                    {
                        GameObject symbolBackground = Resources.Load<GameObject>($"{symbolPath}symbol-background");

                        symbolBackgrounds2.Add(Instantiate(symbolBackground, parent.transform));
                    }

                    symbolBackgrounds2[0].transform.localPosition = new Vector3(Reel.Reel2X, row1Y, 1f);
                    symbolBackgrounds2[1].transform.localPosition = new Vector3(Reel.Reel2X, row2Y, 1f);
                    symbolBackgrounds2[2].transform.localPosition = new Vector3(Reel.Reel2X, row3Y, 1f);
                    symbolBackgrounds2[3].transform.localPosition = new Vector3(Reel.Reel2X, row4Y, 1f);

                    int k = 0;
                    foreach (KeyValuePair<int, Symbol> kvp in determinedSymbols[reel])
                    {
                        GameObject symbol = Resources.Load<GameObject>($"{symbolPath}{kvp.Value}-symbol");
                        Instantiate(symbol, symbolBackgrounds2[k].transform);

                        k++;
                    }

                    break;
                case 2: // reel 3
                    parent = GameObject.FindGameObjectWithTag(TagManager.Reel3);
                    List<GameObject> symbolBackgrounds3 = new List<GameObject>(4);

                    for (int i = 0; i < symbolBackgrounds3.Capacity; i++)
                    {
                        GameObject symbolBackground = Resources.Load<GameObject>($"{symbolPath}symbol-background");

                        symbolBackgrounds3.Add(Instantiate(symbolBackground, parent.transform));
                    }

                    symbolBackgrounds3[0].transform.localPosition = new Vector3(Reel.Reel3X, row1Y, 1f);
                    symbolBackgrounds3[1].transform.localPosition = new Vector3(Reel.Reel3X, row2Y, 1f);
                    symbolBackgrounds3[2].transform.localPosition = new Vector3(Reel.Reel3X, row3Y, 1f);
                    symbolBackgrounds3[3].transform.localPosition = new Vector3(Reel.Reel3X, row4Y, 1f);

                    int l = 0;
                    foreach (KeyValuePair<int, Symbol> kvp in determinedSymbols[reel])
                    {
                        GameObject symbol = Resources.Load<GameObject>($"{symbolPath}{kvp.Value}-symbol");
                        Instantiate(symbol, symbolBackgrounds3[l].transform);

                        l++;
                    }

                    break;
                case 3: // reel 4
                    parent = GameObject.FindGameObjectWithTag(TagManager.Reel4);
                    List<GameObject> symbolBackgrounds4 = new List<GameObject>(4);

                    for (int i = 0; i < symbolBackgrounds4.Capacity; i++)
                    {
                        GameObject symbolBackground = Resources.Load<GameObject>($"{symbolPath}symbol-background");

                        symbolBackgrounds4.Add(Instantiate(symbolBackground, parent.transform));
                    }

                    symbolBackgrounds4[0].transform.localPosition = new Vector3(Reel.Reel4X, row1Y, 1f);
                    symbolBackgrounds4[1].transform.localPosition = new Vector3(Reel.Reel4X, row2Y, 1f);
                    symbolBackgrounds4[2].transform.localPosition = new Vector3(Reel.Reel4X, row3Y, 1f);
                    symbolBackgrounds4[3].transform.localPosition = new Vector3(Reel.Reel4X, row4Y, 1f);

                    int m = 0;
                    foreach (KeyValuePair<int, Symbol> kvp in determinedSymbols[reel])
                    {
                        GameObject symbol = Resources.Load<GameObject>($"{symbolPath}{kvp.Value}-symbol");
                        Instantiate(symbol, symbolBackgrounds4[m].transform);

                        m++;
                    }

                    break;
            }
        }
    }

    // debugging to console
    private void PrintDeterminedSymbols(List<Dictionary<int, Symbol>> determinedSymbols)
    {
        // print each reels' determined symbols for debugging
        for (int reel = 0; reel < determinedSymbols.Count; reel++)
        {
            string symbols = $"Reel {reel + 1} symbols: ";
            foreach (KeyValuePair<int, Symbol> kvp in determinedSymbols[reel])
            {
                symbols += $"{kvp.Key}: {kvp.Value}, ";
            }

            print(symbols);
        }
    }
    
    #endregion


    public IEnumerator SpinReels()
    {
        float delay = 0.1f;

        _reel1.Spin(GetSymbolBackgrounds(1), _spinDuration, RemoveSymbolsOnComplete());

        yield return new WaitForSeconds(delay);

        _reel2.Spin(GetSymbolBackgrounds(2), _spinDuration, RemoveSymbolsOnComplete());

        yield return new WaitForSeconds(delay);

        _reel3.Spin(GetSymbolBackgrounds(3), _spinDuration, RemoveSymbolsOnComplete());

        yield return new WaitForSeconds(delay);

        _reel4.Spin(GetSymbolBackgrounds(4), _spinDuration, RemoveSymbolsOnComplete(isLastReel: true));
    }

    private void CalculateWins()
    {
        List<GameObject> reel1Symbols = GetChildGameObjects(GetSymbolBackgrounds(reel:1));
        List<GameObject> reel2Symbols = GetChildGameObjects(GetSymbolBackgrounds(reel: 2));
        List<GameObject> reel3Symbols = GetChildGameObjects(GetSymbolBackgrounds(reel: 3));
        List<GameObject> reel4Symbols = GetChildGameObjects(GetSymbolBackgrounds(reel: 4));

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

            MaxWinEvent?.Invoke(5000);
        }

        State = State.Ready;
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

    private Action<List<GameObject>> RemoveSymbolsOnComplete(bool isLastReel = false)
    {
        return (symbolBackgrounds) =>
        {
            symbolBackgrounds.RemoveAll(symbolBg =>
            {
                bool shouldRemove = Mathf.Approximately(symbolBg.transform.localPosition.y, Reel.Row1LowerY)
                                    || Mathf.Approximately(symbolBg.transform.localPosition.y, Reel.Row2LowerY)
                                    || Mathf.Approximately(symbolBg.transform.localPosition.y, Reel.Row3LowerY)
                                    || Mathf.Approximately(symbolBg.transform.localPosition.y, Reel.Row4LowerY);

                if (shouldRemove)
                {
                    Destroy(symbolBg);
                }

                return shouldRemove;
            });

            if (isLastReel)
            {
                State = State.CalculatingWins;
                print($"Current state: {State}");
            }
        };
    }

    private List<GameObject> GetSymbolBackgrounds(int reel)
    {
        GameObject parent;
        List<GameObject> reelSymbolBackgrounds = new List<GameObject>();

        switch (reel)
        {
            case 1:
                parent = GameObject.FindGameObjectWithTag(TagManager.Reel1);
                break;
            case 2:
                parent = GameObject.FindGameObjectWithTag(TagManager.Reel2);
                break;
            case 3:
                parent = GameObject.FindGameObjectWithTag(TagManager.Reel3);
                break;
            case 4:
                parent = GameObject.FindGameObjectWithTag(TagManager.Reel4);
                break;
            default:
                throw new ArgumentException("Invalid reel number specified");
        }

        foreach (Transform symbolBackground in parent.transform)
        {
            reelSymbolBackgrounds.Add(symbolBackground.gameObject);
        }

        return reelSymbolBackgrounds;
    }

    private List<GameObject> GetChildGameObjects(List<GameObject> parents)
    {
        if (!parents.Any()) throw new ArgumentException($"Parent list cannot be empty");

        List<GameObject> children = new List<GameObject>();

        foreach (GameObject parent in parents)
        {
            foreach (Transform childTransform in parent.transform)
            {
                GameObject child = childTransform.gameObject;

                children.Add(child);
            }
        }

        return children;
    }

    #region Spawn Explicit Symbols

    // TODO: Add a UI menu that allows symbols to be spawned explicitly for testing. I.e., a max win button that will spawn the W,C,K,D symbols.



    #endregion
}
