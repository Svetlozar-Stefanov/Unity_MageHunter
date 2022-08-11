using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuUIDisplay : MonoBehaviour
{
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject dropButton;

    public void Toggle(bool val)
    {
        useButton.SetActive(val);
        dropButton.SetActive(val);
    }

    public void ToggleUseButton(bool val)
    {
        useButton.SetActive(val);
    }

    public void ToggleDropButton(bool val)
    {
        dropButton.SetActive(val);
    }
}
