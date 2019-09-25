using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Factor : ScriptableObject
{
    [Serializable]
    public class Modifier
    {
        public PlayerData.PP pref;
        public float modifierPer;
    }

    public enum Icon {
        a, b, c
    }
    public bool isPositive = true;
    public Icon icon;
    public string title = nameof(Factor);
    [TextArea]
    public string description;
    public List<Modifier> modifiers;
}
