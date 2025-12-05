using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public static class SlotUtils
    {
        public static List<GameObject> GetChildGameObjects(List<GameObject> parents)
        {
            if (!parents.Any()) throw new ArgumentException($"Parent list cannot be empty");

            List<GameObject> children = new List<GameObject>();

            foreach (GameObject parent in parents)
            {
                if (parent == null)
                {
                    Debug.LogWarning("Destroyed symbol detected! Continuing iteration.");
                    continue;
                }

                foreach (Transform childTransform in parent.transform)
                {
                    GameObject child = childTransform.gameObject;

                    children.Add(child);
                }
            }

            if (children.Count > 4)
            {
                Debug.LogError("Destroyed symbol detected!");
            }

            return children;
        }

        public static List<GameObject> GetSymbolBackgrounds(int reel)
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
}