using System;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class ScrollbarStateHandler
    : MonoBehaviour,
    IStateHandler<ScrollbarState>,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    public event Action<ScrollbarState> StateChanged;

    public void OnPointerEnter(PointerEventData eventData) => StateChanged.Invoke(ScrollbarState.Hover);

    public void OnPointerExit(PointerEventData eventData) => StateChanged.Invoke(ScrollbarState.Normal);

    public void OnPointerDown(PointerEventData eventData) => StateChanged.Invoke(ScrollbarState.Pressed);

    public void OnPointerUp(PointerEventData eventData) => StateChanged.Invoke(ScrollbarState.Normal);
}