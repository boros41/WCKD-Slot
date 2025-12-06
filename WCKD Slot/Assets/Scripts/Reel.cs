using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;
using static UnityEngine.GameObject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Reel
{
    #region Properties
    public ReadOnlyCollection<Symbol> Strip { get; }
    private int _stopIndex;
    private readonly List<int> _stopIndices = new List<int>(4); // 4 == visible reel length

    public const float Reel1X = -1.928f;
    public const float Reel2X = -0.6413333f;
    public const float Reel3X = 0.6453333f;
    public const float Reel4X = 1.932f;

    // out of view symbols above the slot machine
    public const float Row1UpperY = 5.8673332f;
    public const float Row2UpperY = 4.7999999f;
    public const float Row3UpperY = 3.7326666f;
    public const float Row4UpperY = 2.6653333f;

    // symbols in the view of the slot machine
    public const float Row1Y = 1.598f;
    public const float Row2Y = 0.5306667f;
    public const float Row3Y = -0.5366666f;
    public const float Row4Y = -1.604f;

    // out of view symbols beneath the slot machine, TODO: these should probably be mirrors of the uppers
    public const float Row1LowerY = -2.6713333f;
    public const float Row2LowerY = -3.7386666f;
    public const float Row3LowerY = -4.8059999f;
    public const float Row4LowerY = -5.8733332f;
    #endregion

    public Reel(ReadOnlyCollection<Symbol> strip)
    {
        Strip = strip;
    }

    public int StripLength()
    {
        return Strip.Count;
    }

    public List<int> StopIndices()
    {
        _stopIndices.Clear();

        //System.Security.Cryptography.RandomNumberGenerator.GetInt32(); // TODO: use this more secure rng
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

    // TODO: Maybe use delegates to reduce duplication?
    public List<int> StopIndices(Symbol symbol)
    {
        _stopIndices.Clear();

        int stopIndex = Strip.IndexOf(symbol);
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

    // Moving these symbol backgrounds will also move the symbols since they are children
    // NOTE: I originally had a sprite for the symbol background parent but not anymore so it'd be best to refactor and move the symbols themselves
    // LeanTween package: https://assetstore.unity.com/packages/tools/animation/leantween-3595?srsltid=AfmBOop2h1UBbe3iDz6dv3jrd3SEZn__c-y-fd95XVgmqjvj3aloBEVS
    public IEnumerator Spin(List<GameObject> symbolBackgrounds, float spinDuration) // TODO: implement turbo spin button
    {
        bool isFinished = false;
        
        // move the upper symbols into view of the slot machine
        LeanTween.moveY(symbolBackgrounds[4], Row1Y, spinDuration).setEase(SingleBounce());
        LeanTween.moveY(symbolBackgrounds[5], Row2Y, spinDuration).setEase(SingleBounce());
        LeanTween.moveY(symbolBackgrounds[6], Row3Y, spinDuration).setEase(SingleBounce());
        LeanTween.moveY(symbolBackgrounds[7], Row4Y, spinDuration)
                 .setEase(SingleBounce())
                 .setOnComplete(() =>
                 {
                     isFinished = true;
                 });

        // move the symbols already in view to out of view beneath the slot machine
        LeanTween.moveY(symbolBackgrounds[0], Row1LowerY, spinDuration).setEase(SingleBounce());
        LeanTween.moveY(symbolBackgrounds[1], Row2LowerY, spinDuration).setEase(SingleBounce());
        LeanTween.moveY(symbolBackgrounds[2], Row3LowerY, spinDuration).setEase(SingleBounce());
        LeanTween.moveY(symbolBackgrounds[3], Row4LowerY, spinDuration).setEase(SingleBounce());

        yield return new WaitUntil(() => isFinished);


        // TODO: Implement sprite mask to prevent of view symbols from being shown. 
    }

    /*  The LeanTween library does not have a preset easing function that I want for this slot machine.
     *  Because of this, I am using an AnimationCurve to define my own small bounce effect for the symbols when they finish moving/tweening.
     *
     *  The presets are defined here: https://easings.net/
     *
     *  I used the website https://easingwizard.com/#0a2m1n80x12C to create my custom bouncing easing function and prompted
     *  GPT-5.1 to generate this method to return the AnimationCurve that represents the bouncing easing function in the website.*/
    private static AnimationCurve SingleBounce()
    {
        var keys = new[]
        {
            new Keyframe(0.00f, 0.00f),  // start
            new Keyframe(0.55f, 1.03f),  // tiny overshoot (+3%)
            new Keyframe(0.72f, 0.98f),  // small dip (-2%)
            new Keyframe(0.88f, 1.00f),  // settle
            new Keyframe(1.00f, 1.00f),  // final value
        };

        var curve = new AnimationCurve(keys);

        for (int i = 0; i < keys.Length; i++)
            curve.SmoothTangents(i, 0f);

        return curve;
    }
}