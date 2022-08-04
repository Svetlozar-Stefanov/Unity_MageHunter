using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragableUIDisplay : MonoBehaviour
{
    [SerializeField] InputReader input;
    [SerializeField] private Canvas canvas;
    [SerializeField] ItemUIDisplay item;

    private Vector2 mousePos = new Vector2();

    private void Awake()
    {
        canvas.transform.root.GetComponent<Canvas>();
        item.GetComponentInChildren<ItemUIDisplay>();
    }

    private void OnEnable()
    {
        input.inMenuMoveMouseEvent += OnMouseMove;
    }

    private void OnDisable()
    {
        input.inMenuMoveMouseEvent -= OnMouseMove;
    }

    private void OnMouseMove(Vector2 vector2)
    {
        mousePos = vector2;
    }

    public void SetData(Sprite image, int amount)
    {
        item.SetUp(image, amount);
    }

    public void SetData(Sprite image, string amount)
    {
        item.SetUp(image, amount);
    }

    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            mousePos,
            canvas.worldCamera,
            out position);

        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        gameObject.SetActive(val);
    }

    
}
