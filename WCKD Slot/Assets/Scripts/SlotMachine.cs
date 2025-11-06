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
                Symbols.LilyPad, Symbols.Flower, Symbols.Tree, Symbols.Owl, Symbols.LilyPad, Symbols.Bat, Symbols.Flower, Symbols.Skull, Symbols.Flower, Symbols.Tree, Symbols.Moth, Symbols.Frog, Symbols.Flower, Symbols.Crocodile, Symbols.LilyPad, Symbols.Owl, Symbols.Tree, Symbols.Raven, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.Tree, Symbols.Spider, Symbols.Tree, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Bat, Symbols.Flower, Symbols.Spider, Symbols.Crocodile, Symbols.Clover, Symbols.Flower, Symbols.Owl, Symbols.Flower, Symbols.LilyPad, Symbols.Raven, Symbols.Tree, Symbols.Flower, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Moth, Symbols.Tree, Symbols.Tree, Symbols.Skull, Symbols.Owl, Symbols.W, Symbols.LilyPad, Symbols.Bat, Symbols.Tree, Symbols.LilyPad, Symbols.Crocodile, Symbols.Rose, Symbols.LilyPad, Symbols.Bat, Symbols.Rose, Symbols.LilyPad, Symbols.Spider, Symbols.Moth, Symbols.LilyPad, Symbols.Raven, Symbols.Spider, Symbols.Tree
            }
        ));

        _reel2 = new Reel(new ReadOnlyCollection<Symbols>(
            new List<Symbols>()
            {
                Symbols.LilyPad, Symbols.Flower, Symbols.Tree, Symbols.Owl, Symbols.LilyPad, Symbols.Bat, Symbols.Flower, Symbols.Skull, Symbols.Flower, Symbols.Tree, Symbols.Moth, Symbols.LilyPad, Symbols.Crocodile, Symbols.Flower, Symbols.LilyPad, Symbols.Spider, Symbols.Tree, Symbols.Raven, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.Tree, Symbols.Owl, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Bat, Symbols.LilyPad, Symbols.Spider, Symbols.Crocodile, Symbols.Clover, Symbols.Flower, Symbols.Owl, Symbols.LilyPad, Symbols.Flower, Symbols.Raven, Symbols.Tree, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Moth, Symbols.Tree, Symbols.Tree, Symbols.Skull, Symbols.Owl, Symbols.C, Symbols.LilyPad, Symbols.Bat, Symbols.Tree, Symbols.LilyPad, Symbols.Crocodile, Symbols.Rose, Symbols.LilyPad, Symbols.Bat, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Spider, Symbols.Moth, Symbols.Flower, Symbols.Raven, Symbols.Flower, Symbols.LilyPad, Symbols.Owl, Symbols.Tree, Symbols.Spider, Symbols.Flower, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Bat, Symbols.Tree, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.Moth, Symbols.Tree, Symbols.Spider, Symbols.Flower, Symbols.LilyPad, Symbols.Tree, Symbols.Frog, Symbols.Tree, Symbols.Tree, Symbols.Tree
            }
        ));

        _reel3 = new Reel(new ReadOnlyCollection<Symbols>(
            new List<Symbols>()
            {
                Symbols.LilyPad, Symbols.Flower, Symbols.Tree, Symbols.Bat, Symbols.LilyPad, Symbols.Owl, Symbols.Flower, Symbols.Skull, Symbols.Flower, Symbols.Tree, Symbols.Moth, Symbols.LilyPad, Symbols.Crocodile, Symbols.Flower, Symbols.LilyPad, Symbols.Spider, Symbols.Tree, Symbols.Raven, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.Tree, Symbols.Owl, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Bat, Symbols.LilyPad, Symbols.Spider, Symbols.Crocodile, Symbols.Clover, Symbols.Flower, Symbols.Owl, Symbols.LilyPad, Symbols.Flower, Symbols.Raven, Symbols.Tree, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Moth, Symbols.Tree, Symbols.Tree, Symbols.Skull, Symbols.Owl, Symbols.K, Symbols.LilyPad, Symbols.Bat, Symbols.Tree, Symbols.LilyPad, Symbols.Crocodile, Symbols.Rose, Symbols.LilyPad, Symbols.Bat, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Spider, Symbols.Moth, Symbols.Flower, Symbols.Raven, Symbols.Flower, Symbols.LilyPad, Symbols.Owl, Symbols.Tree, Symbols.Spider, Symbols.Flower, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Owl, Symbols.Tree, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.Moth, Symbols.Tree, Symbols.Spider, Symbols.Flower, Symbols.Tree, Symbols.Frog, Symbols.Tree, Symbols.Tree, Symbols.Tree, Symbols.Tree
            }
        ));

        _reel4 = new Reel(new ReadOnlyCollection<Symbols>(
            new List<Symbols>()
            {
                Symbols.LilyPad, Symbols.Flower, Symbols.Tree, Symbols.Bat, Symbols.LilyPad, Symbols.Owl, Symbols.Flower, Symbols.Skull, Symbols.Flower, Symbols.Tree, Symbols.Moth, Symbols.LilyPad, Symbols.Crocodile, Symbols.Flower, Symbols.LilyPad, Symbols.Spider, Symbols.Tree, Symbols.Raven, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.Tree, Symbols.Owl, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Bat, Symbols.LilyPad, Symbols.Spider, Symbols.Crocodile, Symbols.Clover, Symbols.Flower, Symbols.Owl, Symbols.LilyPad, Symbols.Flower, Symbols.Raven, Symbols.Tree, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Moth, Symbols.Tree, Symbols.Tree, Symbols.Skull, Symbols.Owl, Symbols.D, Symbols.LilyPad, Symbols.Bat, Symbols.Tree, Symbols.LilyPad, Symbols.Crocodile, Symbols.Rose, Symbols.LilyPad, Symbols.Bat, Symbols.Rose, Symbols.Flower, Symbols.LilyPad, Symbols.Spider, Symbols.Moth, Symbols.Flower, Symbols.Raven, Symbols.Flower, Symbols.LilyPad, Symbols.Owl, Symbols.Tree, Symbols.Spider, Symbols.Flower, Symbols.LilyPad, Symbols.Skull, Symbols.Flower, Symbols.Bat, Symbols.Tree, Symbols.LilyPad, Symbols.Rose, Symbols.Flower, Symbols.Moth, Symbols.Tree, Symbols.Spider, Symbols.Flower, Symbols.LilyPad, Symbols.Tree, Symbols.Frog, Symbols.Tree, Symbols.Tree, Symbols.Tree, Symbols.LilyPad, Symbols.Crocodile, Symbols.Flower, Symbols.LilyPad, Symbols.Raven, Symbols.LilyPad, Symbols.Skull, Symbols.LilyPad, Symbols.Bat, Symbols.Owl, Symbols.Spider, Symbols.Rose, Symbols.Moth, Symbols.Flower
            }
        ));

        print($"Reel 1 strip length: {_reel1.StripLength()}");
        print($"Reel 2 strip length: {_reel2.StripLength()}");
        print($"Reel 3 strip length: {_reel3.StripLength()}");
        print($"Reel 4 strip length: {_reel4.StripLength()}");

        PopulateStartingSymbols();
    }

    private void PopulateStartingSymbols()
    {
        print($"Reel 1 stop indices: {string.Join(", ", _reel1.StopIndices())}");

        List<int> stopIndices = _reel1.StopIndices();
        foreach (int i in stopIndices)
        {
            print(_reel1.Strip[i]);

            // TODO: instantiate respective symbol from _reel1.StopIndices(), do the same for other reels
        }
    }
}
