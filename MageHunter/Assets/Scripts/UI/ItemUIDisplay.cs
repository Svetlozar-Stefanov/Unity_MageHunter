using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIDisplay : MonoBehaviour
{
    [SerializeField] private Image itemGraphic;
    [SerializeField] private TextMeshProUGUI amountText;

    public void SetUp(Sprite graphic, string text)
    {
        itemGraphic.sprite = graphic;
        amountText.text = text;
    }

    public void SetText(string text)
    {
        amountText.text = text;
    }
}
