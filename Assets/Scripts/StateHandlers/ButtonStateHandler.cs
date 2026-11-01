using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class ButtonStateHandler
    : MonoBehaviour,
    IStateHandler<ButtonState>,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    private Button _button;

    private bool _lastInteractable = true;

    public event Action<ButtonState> StateChanged;

    private void Awake() => _button = GetComponent<Button>();

    private void Update()
    {
        if (_button.interactable == _lastInteractable) return;

        _lastInteractable = _button.interactable;

        SetState(_lastInteractable ? ButtonState.Normal : ButtonState.Disabled);
    }

    public void OnPointerEnter(PointerEventData eventData) => SetState(ButtonState.Hover);

    public void OnPointerExit(PointerEventData eventData) => SetState(ButtonState.Normal);

    public void OnPointerDown(PointerEventData eventData) => SetState(ButtonState.Pressed);

    public void OnPointerUp(PointerEventData eventData) => SetState(ButtonState.Normal);

    private void SetState(ButtonState state)
    {
        if (!_button.interactable && state != ButtonState.Disabled) return;

        StateChanged.Invoke(state);
    }
}