using System;
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


    private void Start()
    {
        _reel1 = new Reel(new ReadOnlyCollection<Symbols>(
            new List<Symbols>()
            {
                Symbols.LilyPad, Symbols.Flower, Symbols.DeadTree, Symbols.Owl, Symbols.LilyPad, Symbols.Bat, Symbols.Flower, Symbols.Skull, Symbols.Flower, Symbols.DeadTree, Symbols.Moth, Symbols.Frog, Symbols.Flower, Symbols.Crocodile, Symbols.LilyPad, Symbols.Owl, Symbols.DeadTree, Symbols.Raven, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.DeadTree, Symbols.Spider, Symbols.DeadTree, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Bat, Symbols.Flower, Symbols.Spider, Symbols.Crocodile, Symbols.Clover, Symbols.Flower, Symbols.Owl, Symbols.Flower, Symbols.LilyPad, Symbols.Raven, Symbols.DeadTree, Symbols.Flower, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Moth, Symbols.DeadTree, Symbols.DeadTree, Symbols.Skull, Symbols.Owl, Symbols.W, Symbols.LilyPad, Symbols.Bat, Symbols.DeadTree, Symbols.LilyPad, Symbols.Crocodile, Symbols.Rose, Symbols.LilyPad, Symbols.Bat, Symbols.Rose, Symbols.LilyPad, Symbols.Spider, Symbols.Moth, Symbols.LilyPad, Symbols.Raven, Symbols.Spider, Symbols.DeadTree
            }
        ));

        _reel2 = new Reel(new ReadOnlyCollection<Symbols>(
            new List<Symbols>()
            {
                Symbols.LilyPad, Symbols.Flower, Symbols.DeadTree, Symbols.Owl, Symbols.LilyPad, Symbols.Bat, Symbols.Flower, Symbols.Skull, Symbols.Flower, Symbols.DeadTree, Symbols.Moth, Symbols.LilyPad, Symbols.Crocodile, Symbols.Flower, Symbols.LilyPad, Symbols.Spider, Symbols.DeadTree, Symbols.Raven, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.DeadTree, Symbols.Owl, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Bat, Symbols.LilyPad, Symbols.Spider, Symbols.Crocodile, Symbols.Clover, Symbols.Flower, Symbols.Owl, Symbols.LilyPad, Symbols.Flower, Symbols.Raven, Symbols.DeadTree, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Moth, Symbols.DeadTree, Symbols.DeadTree, Symbols.Skull, Symbols.Owl, Symbols.C, Symbols.LilyPad, Symbols.Bat, Symbols.DeadTree, Symbols.LilyPad, Symbols.Crocodile, Symbols.Rose, Symbols.LilyPad, Symbols.Bat, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Spider, Symbols.Moth, Symbols.Flower, Symbols.Raven, Symbols.Flower, Symbols.LilyPad, Symbols.Owl, Symbols.DeadTree, Symbols.Spider, Symbols.Flower, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Bat, Symbols.DeadTree, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.Moth, Symbols.DeadTree, Symbols.Spider, Symbols.Flower, Symbols.LilyPad, Symbols.DeadTree, Symbols.Frog, Symbols.DeadTree, Symbols.DeadTree, Symbols.DeadTree
            }
        ));

        _reel3 = new Reel(new ReadOnlyCollection<Symbols>(
            new List<Symbols>()
            {
                Symbols.LilyPad, Symbols.Flower, Symbols.DeadTree, Symbols.Bat, Symbols.LilyPad, Symbols.Owl, Symbols.Flower, Symbols.Skull, Symbols.Flower, Symbols.DeadTree, Symbols.Moth, Symbols.LilyPad, Symbols.Crocodile, Symbols.Flower, Symbols.LilyPad, Symbols.Spider, Symbols.DeadTree, Symbols.Raven, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.DeadTree, Symbols.Owl, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Bat, Symbols.LilyPad, Symbols.Spider, Symbols.Crocodile, Symbols.Clover, Symbols.Flower, Symbols.Owl, Symbols.LilyPad, Symbols.Flower, Symbols.Raven, Symbols.DeadTree, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Moth, Symbols.DeadTree, Symbols.DeadTree, Symbols.Skull, Symbols.Owl, Symbols.K, Symbols.LilyPad, Symbols.Bat, Symbols.DeadTree, Symbols.LilyPad, Symbols.Crocodile, Symbols.Rose, Symbols.LilyPad, Symbols.Bat, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Spider, Symbols.Moth, Symbols.Flower, Symbols.Raven, Symbols.Flower, Symbols.LilyPad, Symbols.Owl, Symbols.DeadTree, Symbols.Spider, Symbols.Flower, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Owl, Symbols.DeadTree, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.Moth, Symbols.DeadTree, Symbols.Spider, Symbols.Flower, Symbols.DeadTree, Symbols.Frog, Symbols.DeadTree, Symbols.DeadTree, Symbols.DeadTree, Symbols.DeadTree
            }
        ));

        _reel4 = new Reel(new ReadOnlyCollection<Symbols>(
            new List<Symbols>()
            {
                Symbols.LilyPad, Symbols.Flower, Symbols.DeadTree, Symbols.Bat, Symbols.LilyPad, Symbols.Owl, Symbols.Flower, Symbols.Skull, Symbols.Flower, Symbols.DeadTree, Symbols.Moth, Symbols.LilyPad, Symbols.Crocodile, Symbols.Flower, Symbols.LilyPad, Symbols.Spider, Symbols.DeadTree, Symbols.Raven, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.DeadTree, Symbols.Owl, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Bat, Symbols.LilyPad, Symbols.Spider, Symbols.Crocodile, Symbols.Clover, Symbols.Flower, Symbols.Owl, Symbols.LilyPad, Symbols.Flower, Symbols.Raven, Symbols.DeadTree, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Moth, Symbols.DeadTree, Symbols.DeadTree, Symbols.Skull, Symbols.Owl, Symbols.D, Symbols.LilyPad, Symbols.Bat, Symbols.DeadTree, Symbols.LilyPad, Symbols.Crocodile, Symbols.Rose, Symbols.LilyPad, Symbols.Bat, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Spider, Symbols.Moth, Symbols.Flower, Symbols.Raven, Symbols.Flower, Symbols.LilyPad, Symbols.Owl, Symbols.DeadTree, Symbols.Spider, Symbols.Flower, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Bat, Symbols.DeadTree, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.Moth, Symbols.DeadTree, Symbols.Spider, Symbols.Flower, Symbols.LilyPad, Symbols.DeadTree, Symbols.Frog, Symbols.DeadTree, Symbols.DeadTree, Symbols.DeadTree, Symbols.LilyPad, Symbols.Crocodile, Symbols.Flower, Symbols.LilyPad, Symbols.Raven, Symbols.LilyPad, Symbols.Skull, Symbols.LilyPad, Symbols.Bat, Symbols.Owl, Symbols.Spider, Symbols.Rose, Symbols.Moth, Symbols.Flower
            }
        ));

        _reel1.PrintSymbols(1);
        _reel2.PrintSymbols(2);
        _reel3.PrintSymbols(3);
        _reel4.PrintSymbols(4);

        PopulateStartingSymbols();
    }

    private void PopulateStartingSymbols()
    {
        List<Reel> reels = new List<Reel>() { _reel1, _reel2, _reel3, _reel4 };
        List<List<int>> stopIndices = new List<List<int>>() { _reel1.StopIndices(), _reel2.StopIndices(), _reel3.StopIndices(), _reel4.StopIndices() };
        List<Dictionary<int, Symbols>> determinedSymbols = new List<Dictionary<int, Symbols>>(4) {new(4), new(4) , new(4) , new(4) };

        // store determined symbols of each reel with their index (indexed from Reel.Strip list)
        for (int reel = 0; reel < determinedSymbols.Count; reel++)
        {
            foreach (int i in stopIndices[reel])
            {
                determinedSymbols[reel].Add(i, reels[reel].Strip[i]);
            }
        }

        PrintDeterminedSymbols(determinedSymbols);

        PopulateReels(determinedSymbols);
    }

    private void PopulateReels(List<Dictionary<int, Symbols>> determinedSymbols)
    {
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

                    symbolBackgrounds1[0].transform.localPosition = new Vector3(Reel.Reel1X, Reel.Row1Y, 1f);
                    symbolBackgrounds1[1].transform.localPosition = new Vector3(Reel.Reel1X, Reel.Row2Y, 1f);
                    symbolBackgrounds1[2].transform.localPosition = new Vector3(Reel.Reel1X, Reel.Row3Y, 1f);
                    symbolBackgrounds1[3].transform.localPosition = new Vector3(Reel.Reel1X, Reel.Row4Y, 1f);

                    int j = 0;
                    foreach (KeyValuePair<int, Symbols> kvp in determinedSymbols[r])
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

                    symbolBackgrounds2[0].transform.localPosition = new Vector3(Reel.Reel2X, Reel.Row1Y, 1f);
                    symbolBackgrounds2[1].transform.localPosition = new Vector3(Reel.Reel2X, Reel.Row2Y, 1f);
                    symbolBackgrounds2[2].transform.localPosition = new Vector3(Reel.Reel2X, Reel.Row3Y, 1f);
                    symbolBackgrounds2[3].transform.localPosition = new Vector3(Reel.Reel2X, Reel.Row4Y, 1f);

                    int k = 0;
                    foreach (KeyValuePair<int, Symbols> kvp in determinedSymbols[r])
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

                    symbolBackgrounds3[0].transform.localPosition = new Vector3(Reel.Reel3X, Reel.Row1Y, 1f);
                    symbolBackgrounds3[1].transform.localPosition = new Vector3(Reel.Reel3X, Reel.Row2Y, 1f);
                    symbolBackgrounds3[2].transform.localPosition = new Vector3(Reel.Reel3X, Reel.Row3Y, 1f);
                    symbolBackgrounds3[3].transform.localPosition = new Vector3(Reel.Reel3X, Reel.Row4Y, 1f);

                    int l = 0;
                    foreach (KeyValuePair<int, Symbols> kvp in determinedSymbols[r])
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

                    symbolBackgrounds4[0].transform.localPosition = new Vector3(Reel.Reel4X, Reel.Row1Y, 1f);
                    symbolBackgrounds4[1].transform.localPosition = new Vector3(Reel.Reel4X, Reel.Row2Y, 1f);
                    symbolBackgrounds4[2].transform.localPosition = new Vector3(Reel.Reel4X, Reel.Row3Y, 1f);
                    symbolBackgrounds4[3].transform.localPosition = new Vector3(Reel.Reel4X, Reel.Row4Y, 1f);

                    int m = 0;
                    foreach (KeyValuePair<int, Symbols> kvp in determinedSymbols[r])
                    {
                        Instantiate(Resources.Load<GameObject>($"Prefabs/{kvp.Value}-symbol"), symbolBackgrounds4[m].transform);

                        m++;
                    }

                    break;
            }
        }
    }

    private void PrintDeterminedSymbols(List<Dictionary<int, Symbols>> determinedSymbols)
    {
        // print each reels' determined symbols for debugging
        for (int reel = 0; reel < determinedSymbols.Count; reel++)
        {
            string symbols = $"Reel {reel + 1} symbols: ";
            foreach (KeyValuePair<int, Symbols> kvp in determinedSymbols[reel])
            {
                symbols += $"{kvp.Key}: {kvp.Value}, ";
            }

            print(symbols);
        }
    }
}
