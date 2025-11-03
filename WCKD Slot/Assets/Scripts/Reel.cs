using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class Reel
{
    private readonly ReadOnlyCollection<Symbols> _strip;
    private int _stopIndex;

    public Reel(ReadOnlyCollection<Symbols> strip)
    {
        _strip = strip;
    }

    public int StripLength()
    {
        return _strip.Count;
    }
}
