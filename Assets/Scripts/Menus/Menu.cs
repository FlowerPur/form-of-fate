using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MenuAnimator))]
public abstract class Menu : MonoBehaviour, IStateHandler<MenuState>
{
    private MenuAnimator _animator;

    public event Action<MenuState> StateChanged;

    protected virtual void Awake() => _animator = GetComponent<MenuAnimator>();

    protected void SwitchTo(Menu menu) => StartCoroutine(TransitionRoutine(menu));

    private IEnumerator TransitionRoutine(Menu menu)
    {
        StateChanged?.Invoke(MenuState.Close);

        while (_animator.Progress < 1f)
            yield return null;

        menu.gameObject.SetActive(true);
        StateChanged?.Invoke(MenuState.Open);
        gameObject.SetActive(false);
    }
}
