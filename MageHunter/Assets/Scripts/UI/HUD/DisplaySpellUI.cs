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

        if (playerController.FightingComponent.CurrentLightSpell == null)
        {
            lightSpell.enabled = false;
        }
        else
        {
            lightSpell.enabled = true;
            lightSpell.sprite = playerController.FightingComponent.CurrentLightSpell.icon;
        }

        if (playerController.FightingComponent.CurrentHeavySpell == null)
        {
            heavySpell.enabled = false;
        }
        else
        {
            heavySpell.enabled = true;
            heavySpell.sprite = playerController.FightingComponent.CurrentHeavySpell.icon;
        }
    }
}
