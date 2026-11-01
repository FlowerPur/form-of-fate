using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(SliderStateHandler))]
public sealed class SliderAnimator : StateAnimator<
    SliderAnimatorSettings,
    SliderState,
    SliderAnimationSettings,
    SliderStateHandler,
    SliderAnimation>
{
    [SerializeField] private Image _fillImage;

    private RectTransform _rectTransform;
    private Image _image;

    protected override void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        base.Awake();
    }

    protected override SliderAnimation CreateAnimation() =>
        new(
            _rectTransform,
            _image,
            _fillImage,
            Settings.Sprites,
            Settings.FillSprites);

    protected override SliderAnimationSettings GetStateAnimationSettings(SliderState state) =>
        state switch
        {
            SliderState.Normal => Settings.NormalAnimationSettings,
            SliderState.Hover => Settings.HoverAnimationSettings,
            SliderState.Pressed => Settings.PressedAnimationSettings,
            _ => Settings.NormalAnimationSettings
        };
}