using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType
{
    Default,
    LightSpell,
    HeavySpell
}

public class SpellItemUIDisplay : BaseItemUIDisplay
{
    private SlotType type;

    public SlotType Type
    {
        get { return type; }
        set { type = value; }
    }
}
