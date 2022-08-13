using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealthBar : MonoBehaviour
{
    [SerializeField] private HealthComponent health;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        slider.maxValue = health.MaxHealth;
        slider.minValue = 0;
    }

    private void Update()
    {
        slider.value = health.Health;
    }
}
