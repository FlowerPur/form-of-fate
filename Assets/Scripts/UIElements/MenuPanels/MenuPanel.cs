using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public abstract class MenuPanel : MonoBehaviour
{
    private ScrollRect _scrollRect;

    protected virtual void Awake() => _scrollRect = GetComponent<ScrollRect>();

    protected virtual void OnEnable() => _scrollRect.verticalNormalizedPosition = 1f;
}
