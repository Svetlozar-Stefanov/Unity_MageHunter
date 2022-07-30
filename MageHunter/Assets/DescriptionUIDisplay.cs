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

    public void Set(BaseItem item)
    {
        itemGraphic.sprite = item.icon;
        nameText.text = item.name;
        descText.text = item.description;
    }
}
