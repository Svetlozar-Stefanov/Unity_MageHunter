using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionUIDisplay : MonoBehaviour
{
    [SerializeField] private Image itemGraphic;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;

    private void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        itemGraphic.gameObject.SetActive(false);
        nameText.text = "";
        descText.text = "";
    }

    public void Set(Sprite graphic, string name, string desc)
    {
        itemGraphic.gameObject.SetActive(true);
        itemGraphic.sprite = graphic;
        nameText.text = name;
        descText.text = desc;
    }
}
