using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(ScrollbarStateHandler))]
public sealed class ScrollbarAnimator : StateAnimator<
    ScrollbarAnimatorSettings,
    ScrollbarState,
    ScrollbarAnimationSettings,
    ScrollbarStateHandler,
    ScrollbarAnimation>
{
    [SerializeField] private Image _handleImage;

    private RectTransform _rectTransform;
    private Image _image;

    protected override void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        base.Awake();
    }

    protected override ScrollbarAnimation CreateAnimation() =>
        new(
            _rectTransform,
            _image,
            _handleImage,
            Settings.Sprites,
            Settings.HandleSprites);

    protected override ScrollbarAnimationSettings GetStateAnimationSettings(ScrollbarState state) =>
        state switch
        {
            ScrollbarState.Normal => Settings.NormalAnimationSettings,
            ScrollbarState.Hover => Settings.HoverAnimationSettings,
            ScrollbarState.Pressed => Settings.PressedAnimationSettings,
            _ => Settings.NormalAnimationSettings
        };
}
