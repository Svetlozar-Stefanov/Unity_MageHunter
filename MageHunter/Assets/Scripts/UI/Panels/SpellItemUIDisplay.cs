using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
