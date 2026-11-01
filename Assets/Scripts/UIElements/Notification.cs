using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NotificationAnimator))]
public sealed class Notification : MonoBehaviour, IStateHandler<NotificationState>
{
    [SerializeField] private GameObject _content;
    [SerializeField] private Button _okButton;

    private NotificationAnimator _animator;

    public event Action OkButtonClicked;

    public event Action<NotificationState> StateChanged;

    private void Awake() => _animator = GetComponent<NotificationAnimator>();

    private void OnEnable() => _okButton.onClick.AddListener(OnOkButtonClick);

    private void OnDisable() => _okButton.onClick.RemoveListener(OnOkButtonClick);

    public void Open() => StartCoroutine(Transition(NotificationState.Open));

    private void Close() => StartCoroutine(Transition(NotificationState.Close));

    private void OnOkButtonClick()
    {
        OkButtonClicked?.Invoke();
        Close();
    }

    private IEnumerator Transition(NotificationState state)
    {
        if (state != NotificationState.Close)
            _content.SetActive(true);

        StateChanged.Invoke(state);

        while (_animator.Progress < 1f)
            yield return null;

        if (state == NotificationState.Close)
            _content.SetActive(false);
    }
}
