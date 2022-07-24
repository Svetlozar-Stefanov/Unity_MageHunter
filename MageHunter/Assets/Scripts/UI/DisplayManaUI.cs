using UnityEngine;
using UnityEngine.UI;

public class DisplayManaUI : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        slider.maxValue = player.FightingComponent.MaxMana;
        slider.minValue = 0;
    }

    private void Update()
    {
        slider.value = player.FightingComponent.Mana;
    }
}
