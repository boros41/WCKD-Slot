using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Assets.Scripts;
using JetBrains.Annotations;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    #region Properties
    [SerializeField] private WinManager _winManager;
    [SerializeField] private float _spinDuration = 2.5f;

    public static State State;
    public static WinMode WinMode = WinMode.NormalPlay;

    private Reel _reel1;
    private Reel _reel2;
    private Reel _reel3;
    private Reel _reel4;
    #endregion

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
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Frog), _reel2.StopIndices(Symbol.Frog), _reel3.StopIndices(Symbol.Frog), _reel4.StopIndices(Symbol.Frog)
                };
                break;
            case WinMode.FourLeafClover:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Clover), _reel2.StopIndices(Symbol.Clover), _reel3.StopIndices(Symbol.Clover), _reel4.StopIndices(Symbol.Clover)
                };
                break;
            case WinMode.FourSkulls:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Skull), _reel2.StopIndices(Symbol.Skull), _reel3.StopIndices(Symbol.Skull), _reel4.StopIndices(Symbol.Skull)
                };
                break;
            case WinMode.ThreeSkulls:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Skull), _reel2.StopIndices(Symbol.Skull), _reel3.StopIndices(Symbol.Skull), _reel4.StopIndices()
                };
                break;
            case WinMode.FourCrocodiles:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Crocodile), _reel2.StopIndices(Symbol.Crocodile), _reel3.StopIndices(Symbol.Crocodile), _reel4.StopIndices(Symbol.Crocodile)
                };
                break;
            case WinMode.ThreeCrocodiles:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Crocodile), _reel2.StopIndices(Symbol.Crocodile), _reel3.StopIndices(Symbol.Crocodile), _reel4.StopIndices()
                };
                break;
            case WinMode.FourRavens:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Raven), _reel2.StopIndices(Symbol.Raven), _reel3.StopIndices(Symbol.Raven), _reel4.StopIndices(Symbol.Raven)
                };
                break;
            case WinMode.ThreeRavens:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Raven), _reel2.StopIndices(Symbol.Raven), _reel3.StopIndices(Symbol.Raven), _reel4.StopIndices()
                };
                break;
            case WinMode.FourOwls:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Owl), _reel2.StopIndices(Symbol.Owl), _reel3.StopIndices(Symbol.Owl), _reel4.StopIndices(Symbol.Owl)
                };
                break;
            case WinMode.ThreeOwls:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Owl), _reel2.StopIndices(Symbol.Owl), _reel3.StopIndices(Symbol.Owl), _reel4.StopIndices()
                };
                break;
            case WinMode.FourBats:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Bat), _reel2.StopIndices(Symbol.Bat), _reel3.StopIndices(Symbol.Bat), _reel4.StopIndices(Symbol.Bat)
                };
                break;
            case WinMode.ThreeBats:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Bat), _reel2.StopIndices(Symbol.Bat), _reel3.StopIndices(Symbol.Bat), _reel4.StopIndices()
                };
                break;
            case WinMode.FourMoths:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Moth), _reel2.StopIndices(Symbol.Moth), _reel3.StopIndices(Symbol.Moth), _reel4.StopIndices(Symbol.Moth)
                };
                break;
            case WinMode.ThreeMoths:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Moth), _reel2.StopIndices(Symbol.Moth), _reel3.StopIndices(Symbol.Moth), _reel4.StopIndices()
                };
                break;
            case WinMode.FourSpiders:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Spider), _reel2.StopIndices(Symbol.Spider), _reel3.StopIndices(Symbol.Spider), _reel4.StopIndices(Symbol.Spider)
                };
                break;
            case WinMode.ThreeSpiders:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Spider), _reel2.StopIndices(Symbol.Spider), _reel3.StopIndices(Symbol.Spider), _reel4.StopIndices()
                };
                break;
            case WinMode.FourRoses:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Rose), _reel2.StopIndices(Symbol.Rose), _reel3.StopIndices(Symbol.Rose), _reel4.StopIndices(Symbol.Rose)
                };
                break;
            case WinMode.ThreeRoses:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Rose), _reel2.StopIndices(Symbol.Rose), _reel3.StopIndices(Symbol.Rose), _reel4.StopIndices()
                };
                break;
            case WinMode.FourLilyPads:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.LilyPad), _reel2.StopIndices(Symbol.LilyPad), _reel3.StopIndices(Symbol.LilyPad), _reel4.StopIndices(Symbol.LilyPad)
                };
                break;
            case WinMode.ThreeLilyPads:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.LilyPad), _reel2.StopIndices(Symbol.LilyPad), _reel3.StopIndices(Symbol.LilyPad), _reel4.StopIndices()
                };
                break;
            case WinMode.FourFlowers:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Flower), _reel2.StopIndices(Symbol.Flower), _reel3.StopIndices(Symbol.Flower), _reel4.StopIndices(Symbol.Flower)
                };
                break;
            case WinMode.ThreeFlowers:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.Flower), _reel2.StopIndices(Symbol.Flower), _reel3.StopIndices(Symbol.Flower), _reel4.StopIndices()
                };
                break;
            case WinMode.FourDeadTrees:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.DeadTree), _reel2.StopIndices(Symbol.DeadTree), _reel3.StopIndices(Symbol.DeadTree), _reel4.StopIndices(Symbol.DeadTree)
                };
                break;
            case WinMode.ThreeDeadTrees:
                stopIndices = new List<List<int>>()
                {
                    _reel1.StopIndices(Symbol.DeadTree), _reel2.StopIndices(Symbol.DeadTree), _reel3.StopIndices(Symbol.DeadTree), _reel4.StopIndices()
                };
                break;
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
        SlotMachine.State = State.Spinning;
        print($"Starting spin! New state: {SlotMachine.State}");

        // each symbol has a y difference of ~1.0673333 between each other
        PopulateReels(Reel.Row1UpperY, Reel.Row2UpperY, Reel.Row3UpperY, Reel.Row4UpperY);

        float delay = 0.1f;
        bool spinFinished = false;
        List<GameObject> reel1SymbolBgs = SlotUtils.GetSymbolBackgrounds(reel: 1);
        List<GameObject> reel2SymbolBgs = SlotUtils.GetSymbolBackgrounds(reel: 2);
        List<GameObject> reel3SymbolBgs = SlotUtils.GetSymbolBackgrounds(reel: 3);
        List<GameObject> reel4SymbolBgs = SlotUtils.GetSymbolBackgrounds(reel: 4);

        // TODO: refactor callback into lambda
        _reel1.Spin(reel1SymbolBgs, _spinDuration, RemoveBackgroundSymbolsOnComplete());

        yield return new WaitForSeconds(delay);

        _reel2.Spin(reel2SymbolBgs, _spinDuration, RemoveBackgroundSymbolsOnComplete());

        yield return new WaitForSeconds(delay);

        _reel3.Spin(reel3SymbolBgs, _spinDuration, RemoveBackgroundSymbolsOnComplete());

        yield return new WaitForSeconds(delay);

        _reel4.Spin(reel4SymbolBgs, _spinDuration, symbolBackgrounds =>
        {
            RemoveBackgroundSymbolsOnComplete()(symbolBackgrounds);
            spinFinished = true;
        });

        yield return new WaitUntil(() => spinFinished);

        State = State.CalculatingWins;
        print($"Current state: {State}");

        yield return _winManager.CalculateWins();
    }



    private Action<List<GameObject>> RemoveBackgroundSymbolsOnComplete()
    {
        return (symbolBackgrounds) =>
        {
            foreach (GameObject symbolBackground in symbolBackgrounds.GetRange(0, 4))
            {
                symbolBackgrounds.Remove(symbolBackground);
                Destroy(symbolBackground);
            }
        };
    }

    #region Spawn Explicit Symbols

    // TODO: Add a UI menu that allows symbols to be spawned explicitly for testing. I.e., a max win button that will spawn the W,C,K,D symbols.



    #endregion
}
