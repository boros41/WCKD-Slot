using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class Reel
{
    public ReadOnlyCollection<Symbols> Strip { get; }
    private int _stopIndex;
    private readonly List<int> _stopIndices = new List<int>(4); // 4 == visible reel length

    public Reel(ReadOnlyCollection<Symbols> strip)
    {
        Strip = strip;
    }

    public int StripLength()
    {
        return Strip.Count;
    }

    public List<int> StopIndices()
    {
        // TODO: Look into better RNGs for slot machines
        _stopIndices.Clear();

        int stopIndex = Random.Range((int)0, Strip.Count); // represents the symbol in the first row of the visible reels' columns
        _stopIndices.Add(stopIndex);

        int nextIndex = stopIndex + 1; // assume we are not at end of collection
        for (int i = 0; i < 3; i++)
        {

            if (nextIndex == Strip.Count) // end of collection
            {
                nextIndex = 0; // start from beginning
            }

            _stopIndices.Add(nextIndex);

            nextIndex++;
        }

        return _stopIndices;
    }
}
