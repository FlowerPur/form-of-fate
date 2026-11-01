using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class ButtonAnimation<TSettings>
    : BaseAnimation<TSettings>
    where TSettings : IButtonAnimationSettings
{
    private readonly RectTransform _rectTransform;
    private readonly Image _image;

    private readonly Sprite[] _sprites;

    private readonly Vector2 _originalOffsetMin;
    private readonly Vector2 _originalOffsetMax;

    private int _spriteIndex;

    public ButtonAnimation(
        RectTransform rectTransform,
        Image image,
        Sprite[] sprites)
    {
        _rectTransform = rectTransform;
        _image = image;

        _sprites = sprites;

        _originalOffsetMin = rectTransform.offsetMin;
        _originalOffsetMax = rectTransform.offsetMax;
    }

    public override void InitializeSequence(TSettings settings)
    {
        Sequence.Join(CreateSizeTween(settings.SizeOffset, settings.SizeOffsetTweenSettings));
        Sequence.Join(CreateSpriteTween(settings.SpriteIndex, settings.SpriteIndexTweenSettings));
    }

    private Tween CreateSizeTween(float offset, TweenSettings settings) =>
        DOTween.To(
            () => _originalOffsetMin.x - _rectTransform.offsetMin.x,
            value =>
            {
                _rectTransform.offsetMin = _originalOffsetMin - (Vector2.one * value);
                _rectTransform.offsetMax = _originalOffsetMax + (Vector2.one * value);
            },
            offset,
            settings.Duration)
        .SetEase(settings.Ease);

    private Tween CreateSpriteTween(int index, TweenSettings settings) =>
        DOTween.To(
            () => _spriteIndex,
            value =>
            {
                _spriteIndex = value;
                _image.sprite = _sprites[value];
            },
            index,
            settings.Duration)
        .SetEase(settings.Ease);
}