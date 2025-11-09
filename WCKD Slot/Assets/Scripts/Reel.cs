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

    public const float Reel1X = -1.928f;
    public const float Reel2X = -0.6413333f;
    public const float Reel3X = 0.6453333f;
    public const float Reel4X = 1.932f;

    public const float Row1Y = 1.598f;
    public const float Row2Y = 0.5306667f;
    public const float Row3Y = -0.5366666f;
    public const float Row4Y = -1.604f;

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
        // TODO: Look into "more random" RNGs for slot machines
        _stopIndices.Clear();

        int stopIndex = Random.Range((int)0, Strip.Count); // represents the symbol in the first row of the visible reels' columns
        _stopIndices.Add(stopIndex);

        int nextIndex = stopIndex + 1; // assume we are not at end of collection
        for (int i = 0; i < 3; i++) // there are 4 visible rows so we need 3 more
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

    public void PrintSymbols(int reel)
    {
        string numberedSymbols = string.Empty;

        for (int i = 0; i < Strip.Count; i++)
        {
            numberedSymbols += i + ": " + Strip[i] + ", ";
        }

        Debug.Log($"Reel {reel} strip length: {Strip.Count}");
        Debug.Log($"Reel {reel} symbols: {numberedSymbols}");
    }
}
