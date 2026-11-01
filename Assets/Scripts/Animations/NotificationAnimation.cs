using UnityEngine;
using DG.Tweening;

public sealed class NotificationAnimation : BaseAnimation<NotificationAnimationSettings>
{
    private readonly RectTransform _contentRectTransform;
    private readonly PixelEraser _darkening;

    private readonly Vector2 _originalContentOffsetMin;
    private readonly Vector2 _originalContentOffsetMax;

    public NotificationAnimation(RectTransform contentRectTransform, PixelEraser darkening)
    {
        _contentRectTransform = contentRectTransform;
        _darkening = darkening;

        _originalContentOffsetMin = contentRectTransform.offsetMin;
        _originalContentOffsetMax = contentRectTransform.offsetMax;
    }

    public override void InitializeSequence(NotificationAnimationSettings settings)
    {
        Sequence.Join(CreateContentSizeTween(settings.ContentSizeOffset, settings.ContentSizeOffsetTweenSettings));
        Sequence.Join(CreateDarkeningTween(settings.DarkeningProgress, settings.DarkeningProgressTweenSettings));
    }

    private Tween CreateContentSizeTween(float offset, TweenSettings settings) =>
        DOTween.To(
            () => _originalContentOffsetMin.x - _contentRectTransform.offsetMin.x,
            value =>
            {
                _contentRectTransform.offsetMin = _originalContentOffsetMin - (Vector2.one * value);
                _contentRectTransform.offsetMax = _originalContentOffsetMax + (Vector2.one * value);
            },
            offset,
            settings.Duration)
        .SetEase(settings.Ease);

    private Tween CreateDarkeningTween(float progress, TweenSettings settings) =>
        DOTween.To(
            () => _darkening.Progress,
            value => _darkening.Progress = value,
            progress,
            settings.Duration)
        .SetEase(settings.Ease);
}