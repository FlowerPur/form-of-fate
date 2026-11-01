using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public sealed class ShiftEnterInputField : MonoBehaviour, IUpdateSelectedHandler
{
    public event Action<string> SendRequested;

    private TMP_InputField _inputField;

    private void Awake() => _inputField = GetComponent<TMP_InputField>();

    public void OnUpdateSelected(BaseEventData eventData)
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null) return;

        if (!(keyboard.enterKey.wasPressedThisFrame || keyboard.numpadEnterKey.wasPressedThisFrame)) return;

        if (keyboard.leftShiftKey.isPressed || keyboard.rightShiftKey.isPressed)
            InsertNewLine();
        else
            Submit();
    }

    private void Submit()
    {
        string text = _inputField.text;

        if (string.IsNullOrWhiteSpace(text)) return;

        SendRequested?.Invoke(text);

        _inputField.ActivateInputField();
    }

    private void InsertNewLine()
    {
        int caretPosition = _inputField.caretPosition;

        _inputField.text = _inputField.text.Insert(caretPosition, "\n");
        int newCaretPosition = caretPosition + 1;

        _inputField.ActivateInputField();
        _inputField.caretPosition = newCaretPosition;
        _inputField.selectionAnchorPosition = newCaretPosition;
        _inputField.selectionFocusPosition = newCaretPosition;
    }
}