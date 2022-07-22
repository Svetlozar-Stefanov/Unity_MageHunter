using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySpellUI : MonoBehaviour
{
    public Image lightSpellBackPanel;
    public Image heavySpellBackPanel;

    public Image lightSpell;
    public Image heavySpell;

    public PlayerController playerController;

    void Update()
    {
        if (playerController.IsChangingSpellSelector())
        {
            lightSpellBackPanel.color = Color.white;
            heavySpellBackPanel.color = Color.yellow;
        }
        if (!playerController.IsChangingSpellSelector())
        {
            lightSpellBackPanel.color = Color.yellow;
            heavySpellBackPanel.color = Color.white;
        }

        lightSpell.sprite = playerController.FightingComponent.CurrentLightSpell.image;
        heavySpell.sprite = playerController.FightingComponent.CurrentHeavySpell.image;
    }
}
