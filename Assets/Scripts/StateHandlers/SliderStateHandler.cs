using System;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class SliderStateHandler
    : MonoBehaviour,
    IStateHandler<SliderState>,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    public event Action<SliderState> StateChanged;

    public void OnPointerEnter(PointerEventData eventData) => StateChanged.Invoke(SliderState.Hover);

    public void OnPointerExit(PointerEventData eventData) => StateChanged.Invoke(SliderState.Normal);

    public void OnPointerDown(PointerEventData eventData) => StateChanged.Invoke(SliderState.Pressed);

    public void OnPointerUp(PointerEventData eventData) => StateChanged.Invoke(SliderState.Normal);
}