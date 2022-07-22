using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySpellUI : MonoBehaviour
{
    public Image back1;
    public Image back2;

    public Text s1;
    public Text s2;

    public FightingComponent playerSpells;
    public InputReader input;

    void Update()
    {
        if (input.isPressedQ)
        {
            back1.color = Color.white;
            back2.color = Color.yellow;
        }
        if (!input.isPressedQ)
        {
            back1.color = Color.yellow;
            back2.color = Color.white;
        }

        s1.text = playerSpells.lightIdx.ToString();
        s2.text = playerSpells.heavyIdx.ToString();
    }
}
