using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    private Reel _reel1;
    private Reel _reel2;
    private Reel _reel3;
    private Reel _reel4;
    public static State State;

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
        List<List<int>> stopIndices = new List<List<int>>() { _reel1.StopIndices(), _reel2.StopIndices(), _reel3.StopIndices(), _reel4.StopIndices() };
        List<Dictionary<int, Symbol>> determinedSymbols = new List<Dictionary<int, Symbol>>(4) {new(4), new(4) , new(4) , new(4) };

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
    private void PopulateReels(float row1Y, float row2Y, float row3Y, float row4Y)
    {
        List<Dictionary<int, Symbol>> determinedSymbols = DetermineSymbols();

        for (int r = 0; r < 4; r++)
        {
            GameObject parent = null;
            switch (r)
            {
                // TODO: Move each case into a separate method for populating that reel. Look into delegate methods to reduce duplication.
                case 0: // reel 1
                    parent = GameObject.FindGameObjectWithTag(TagManager.Reel1);
                    List<GameObject> symbolBackgrounds1 = new List<GameObject>(4);

                    for (int i = 0; i < symbolBackgrounds1.Capacity; i++)
                    {
                        symbolBackgrounds1.Add(Instantiate(Resources.Load<GameObject>("Prefabs/symbol-background"), parent.transform));
                    }

                    symbolBackgrounds1[0].transform.localPosition = new Vector3(Reel.Reel1X, row1Y, 1f);
                    symbolBackgrounds1[1].transform.localPosition = new Vector3(Reel.Reel1X, row2Y, 1f);
                    symbolBackgrounds1[2].transform.localPosition = new Vector3(Reel.Reel1X, row3Y, 1f);
                    symbolBackgrounds1[3].transform.localPosition = new Vector3(Reel.Reel1X, row4Y, 1f);

                    int j = 0;
                    foreach (KeyValuePair<int, Symbol> kvp in determinedSymbols[r])
                    {
                        Instantiate(Resources.Load<GameObject>($"Prefabs/{kvp.Value}-symbol"), symbolBackgrounds1[j].transform);

                        j++;
                    }

                    break;
                case 1: // reel 2
                    parent = GameObject.FindGameObjectWithTag(TagManager.Reel2);
                    List<GameObject> symbolBackgrounds2 = new List<GameObject>(4);

                    for (int i = 0; i < symbolBackgrounds2.Capacity; i++)
                    {
                        symbolBackgrounds2.Add(Instantiate(Resources.Load<GameObject>("Prefabs/symbol-background"), parent.transform));
                    }

                    symbolBackgrounds2[0].transform.localPosition = new Vector3(Reel.Reel2X, row1Y, 1f);
                    symbolBackgrounds2[1].transform.localPosition = new Vector3(Reel.Reel2X, row2Y, 1f);
                    symbolBackgrounds2[2].transform.localPosition = new Vector3(Reel.Reel2X, row3Y, 1f);
                    symbolBackgrounds2[3].transform.localPosition = new Vector3(Reel.Reel2X, row4Y, 1f);

                    int k = 0;
                    foreach (KeyValuePair<int, Symbol> kvp in determinedSymbols[r])
                    {
                        Instantiate(Resources.Load<GameObject>($"Prefabs/{kvp.Value}-symbol"), symbolBackgrounds2[k].transform);

                        k++;
                    }

                    break;
                case 2: // reel 3
                    parent = GameObject.FindGameObjectWithTag(TagManager.Reel3);
                    List<GameObject> symbolBackgrounds3 = new List<GameObject>(4);

                    for (int i = 0; i < symbolBackgrounds3.Capacity; i++)
                    {
                        symbolBackgrounds3.Add(Instantiate(Resources.Load<GameObject>("Prefabs/symbol-background"), parent.transform));
                    }

                    symbolBackgrounds3[0].transform.localPosition = new Vector3(Reel.Reel3X, row1Y, 1f);
                    symbolBackgrounds3[1].transform.localPosition = new Vector3(Reel.Reel3X, row2Y, 1f);
                    symbolBackgrounds3[2].transform.localPosition = new Vector3(Reel.Reel3X, row3Y, 1f);
                    symbolBackgrounds3[3].transform.localPosition = new Vector3(Reel.Reel3X, row4Y, 1f);

                    int l = 0;
                    foreach (KeyValuePair<int, Symbol> kvp in determinedSymbols[r])
                    {
                        Instantiate(Resources.Load<GameObject>($"Prefabs/{kvp.Value}-symbol"), symbolBackgrounds3[l].transform);

                        l++;
                    }

                    break;
                case 3: // reel 4
                    parent = GameObject.FindGameObjectWithTag(TagManager.Reel4);
                    List<GameObject> symbolBackgrounds4 = new List<GameObject>(4);

                    for (int i = 0; i < symbolBackgrounds4.Capacity; i++)
                    {
                        symbolBackgrounds4.Add(Instantiate(Resources.Load<GameObject>("Prefabs/symbol-background"), parent.transform));
                    }

                    symbolBackgrounds4[0].transform.localPosition = new Vector3(Reel.Reel4X, row1Y, 1f);
                    symbolBackgrounds4[1].transform.localPosition = new Vector3(Reel.Reel4X, row2Y, 1f);
                    symbolBackgrounds4[2].transform.localPosition = new Vector3(Reel.Reel4X, row3Y, 1f);
                    symbolBackgrounds4[3].transform.localPosition = new Vector3(Reel.Reel4X, row4Y, 1f);

                    int m = 0;
                    foreach (KeyValuePair<int, Symbol> kvp in determinedSymbols[r])
                    {
                        Instantiate(Resources.Load<GameObject>($"Prefabs/{kvp.Value}-symbol"), symbolBackgrounds4[m].transform);

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

    public void OnSpinClick()
    {
        if (State != State.Ready)
        {
            print($"Cannot spin while state is \"{State}\"");
            return;
        }

        // each symbol has a y difference of ~1.0673333 between each other
        PopulateReels(Reel.Row1UpperY , Reel.Row2UpperY , Reel.Row3UpperY , Reel.Row4UpperY);

        State = State.Spinning;
        // TODO: play spin sound effect
        print($"Spin button clicked! New state: {State}");


        StartCoroutine(SpinReels());
    }

    private IEnumerator SpinReels()
    {
        float delay = 0.1f;

        _reel1.Spin(GetSymbolBackgrounds(1));

        yield return new WaitForSeconds(delay);

        _reel2.Spin(GetSymbolBackgrounds(2));

        yield return new WaitForSeconds(delay);

        _reel3.Spin(GetSymbolBackgrounds(3));

        yield return new WaitForSeconds(delay);

        _reel4.Spin(GetSymbolBackgrounds(4));

        yield return new WaitForSeconds(delay);
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
}
