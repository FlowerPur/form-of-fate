using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(SelectorAnimator))]
public sealed class Selector : MonoBehaviour, IStateHandler<SelectorState>
{
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private Button _previousButton;
    [SerializeField] private Button _nextButton;

    [SerializeField] private string[] _options;
    [SerializeField] private int _index;

    private SelectorAnimator _animator;

    public event Action<int> IndexChanged;

    public event Action<SelectorState> StateChanged;

    public int Index => _index;

    private int MaxIndex => _options.Length - 1;

    private void Awake() => _animator = GetComponent<SelectorAnimator>();

    private void OnEnable()
    {
        _previousButton.onClick.AddListener(OnPreviousButtonClick);
        _nextButton.onClick.AddListener(OnNextButtonClick);
    }

    private void Start() => UpdateUI();

    private void OnDisable()
    {
        _previousButton.onClick.RemoveListener(OnPreviousButtonClick);
        _nextButton.onClick.RemoveListener(OnNextButtonClick);
    }

    public void SetOptions(string[] options)
    {
        _options = options;

        UpdateUI();
    }

    public void SetIndex(int index)
    {
        if (_index == index) return;

        _index = index;

        UpdateUI();
        IndexChanged?.Invoke(index);
    }

    private void OnPreviousButtonClick() => StartCoroutine(ChangeIndex(direction: -1));

    private void OnNextButtonClick() => StartCoroutine(ChangeIndex(direction: 1));

    private IEnumerator ChangeIndex(int direction)
    {
        SetIndex(_index + direction);

        StateChanged.Invoke(SelectorState.Change);

        while (_animator.Progress < 1f)
            yield return null;

        StateChanged.Invoke(SelectorState.Normal);
    }

    private void UpdateUI()
    {
        _labelText.text = _options[_index];

        _previousButton.interactable = _index != 0;
        _nextButton.interactable = _index != MaxIndex;
    }
}
