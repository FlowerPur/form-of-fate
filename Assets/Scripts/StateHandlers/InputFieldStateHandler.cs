using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(InputFieldAnimator))]
[RequireComponent(typeof(TMP_InputField))]
public sealed class InputFieldStateHandler
    : MonoBehaviour,
    IStateHandler<InputFieldState>,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    private InputFieldAnimator _animator;
    private TMP_InputField _inputField;

    public event Action<InputFieldState> StateChanged;

    private void Awake()
    {
        _animator = GetComponent<InputFieldAnimator>();
        _inputField = GetComponent<TMP_InputField>();
    }

    private void OnEnable() => _inputField.onValueChanged.AddListener(OnValueChanged);

    private void OnDisable() => _inputField.onValueChanged.RemoveListener(OnValueChanged);

    public void OnPointerEnter(PointerEventData eventData) => StateChanged.Invoke(InputFieldState.Hover);

    public void OnPointerExit(PointerEventData eventData) => StateChanged.Invoke(InputFieldState.Normal);

    public void OnPointerDown(PointerEventData eventData) => StateChanged.Invoke(InputFieldState.Pressed);

    public void OnPointerUp(PointerEventData eventData) => StateChanged.Invoke(InputFieldState.Normal);

    private void OnValueChanged(string newValue) => StartCoroutine(SetStateChange());

    private IEnumerator SetStateChange()
    {
        StateChanged.Invoke(InputFieldState.Change);

        while (_animator.Progress < 1)
            yield return null;

        StateChanged.Invoke(InputFieldState.Normal);
    }
}