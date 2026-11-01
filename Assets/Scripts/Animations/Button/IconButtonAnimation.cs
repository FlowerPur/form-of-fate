using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public sealed class IconButtonAnimation : ButtonAnimation<IconButtonAnimationSettings>
{
    private readonly RectTransform _iconRectTransform;

    private readonly Vector2 _originalIconOffsetMin;
    private readonly Vector2 _originalIconOffsetMax;

    public IconButtonAnimation(
        RectTransform rectTransform,
        Image image,
        Sprite[] sprites,
        RectTransform iconRectTransform)
        : base(rectTransform, image, sprites)
    {
        _iconRectTransform = iconRectTransform;

        _originalIconOffsetMin = _iconRectTransform.offsetMin;
        _originalIconOffsetMax = _iconRectTransform.offsetMax;
    }

    public override void InitializeSequence(IconButtonAnimationSettings settings)
    {
        base.InitializeSequence(settings);

        Sequence.Join(CreateIconSizeTween(settings.IconSizeOffset, settings.IconSizeOffsetTweenSettings));
    }

    private Tween CreateIconSizeTween(float offset, TweenSettings settings) =>
        DOTween.To(
            () => _originalIconOffsetMin.x - _iconRectTransform.offsetMin.x,
            value =>
            {
                _iconRectTransform.offsetMin = _originalIconOffsetMin - (Vector2.one * value);
                _iconRectTransform.offsetMax = _originalIconOffsetMax + (Vector2.one * value);
            },
            offset,
            settings.Duration)
        .SetEase(settings.Ease);
}