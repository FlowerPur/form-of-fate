using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public sealed class SliderAnimation : BaseAnimation<SliderAnimationSettings>
{
    private readonly RectTransform _rectTransform;
    private readonly Image _image;
    private readonly Image _fillImage;

    private readonly Sprite[] _sprites;
    private readonly Sprite[] _fillSprites;

    private readonly Vector2 _originalOffsetMin;
    private readonly Vector2 _originalOffsetMax;

    private int _spriteIndex;
    private int _fillSpriteIndex;

    public SliderAnimation(
        RectTransform rectTransform,
        Image image,
        Image fillImage,
        Sprite[] sprites,
        Sprite[] fillSprites)
    {
        _rectTransform = rectTransform;
        _image = image;
        _fillImage = fillImage;

        _sprites = sprites;
        _fillSprites = fillSprites;

        _originalOffsetMin = rectTransform.offsetMin;
        _originalOffsetMax = rectTransform.offsetMax;
    }

    public override void InitializeSequence(SliderAnimationSettings settings)
    {
        Sequence.Join(CreateSizeTween(settings.SizeOffset, settings.SizeOffsetTweenSettings));
        Sequence.Join(CreateSpriteTween(settings.SpriteIndex, settings.SpriteIndexTweenSettings));
        Sequence.Join(CreateFillSpriteTween(settings.FillSpriteIndex, settings.FillSpriteIndexTweenSettings));
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

    private Tween CreateFillSpriteTween(int index, TweenSettings settings) =>
        DOTween.To(
            () => _fillSpriteIndex,
            value =>
            {
                _fillSpriteIndex = value;
                _fillImage.sprite = _fillSprites[value];
            },
            index,
            settings.Duration)
        .SetEase(settings.Ease);
}