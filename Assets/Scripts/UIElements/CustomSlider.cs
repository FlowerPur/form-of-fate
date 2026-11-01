using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CustomSliderAnimator))]
public sealed class CustomSlider : MonoBehaviour, IStateHandler<CustomSliderState>
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _labelText;

    private int _value = 100;

    private CustomSliderAnimator _animator;

    public event Action<int> ValueChanged;

    public event Action<CustomSliderState> StateChanged;

    private void Awake() => _animator = GetComponent<CustomSliderAnimator>();

    private void OnEnable() => _slider.onValueChanged.AddListener(OnSliderValueChanged);

    private void OnDisable() => _slider.onValueChanged.RemoveListener(OnSliderValueChanged);

    public void SetValue(int value)
    {
        if (value == _value) return;

        _value = value;

        UpdateUI();
        ValueChanged?.Invoke(value);
    }

    private void OnSliderValueChanged(float value) => StartCoroutine(ChangeValue((int)value));

    private IEnumerator ChangeValue(int value)
    {
        SetValue(value);

        StateChanged.Invoke(CustomSliderState.Change);

        while (_animator.Progress < 1)
            yield return null;

        StateChanged.Invoke(CustomSliderState.Normal);
    }

    private void UpdateUI()
    {
        _slider.value = _value;
        _labelText.text = _value.ToString();
    }
}
