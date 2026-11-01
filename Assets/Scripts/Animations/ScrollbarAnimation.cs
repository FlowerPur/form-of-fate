using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public sealed class ScrollbarAnimation : BaseAnimation<ScrollbarAnimationSettings>
{
    private readonly RectTransform _rectTransform;
    private readonly Image _image;
    private readonly Image _handleImage;

    private readonly Sprite[] _sprites;
    private readonly Sprite[] _handleSprites;

    private readonly Vector2 _originalOffsetMin;
    private readonly Vector2 _originalOffsetMax;

    private int _spriteIndex;
    private int _handleSpriteIndex;

    public ScrollbarAnimation(
        RectTransform rectTransform,
        Image image,
        Image handleImage,
        Sprite[] sprites,
        Sprite[] handleSprites)
    {
        _rectTransform = rectTransform;
        _image = image;
        _handleImage = handleImage;

        _sprites = sprites;
        _handleSprites = handleSprites;

        _originalOffsetMin = rectTransform.offsetMin;
        _originalOffsetMax = rectTransform.offsetMax;
    }

    public override void InitializeSequence(ScrollbarAnimationSettings settings)
    {
        Sequence.Join(CreateSizeTween(settings.SizeOffset, settings.SizeOffsetTweenSettings));
        Sequence.Join(CreateSpriteTween(settings.SpriteIndex, settings.SpriteIndexTweenSettings));
        Sequence.Join(CreateHandleSpriteTween(settings.HandleSpriteIndex, settings.HandleSpriteIndexTweenSettings));
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

    private Tween CreateHandleSpriteTween(int index, TweenSettings settings) =>
        DOTween.To(
            () => _handleSpriteIndex,
            value =>
            {
                _handleSpriteIndex = value;
                _handleImage.sprite = _handleSprites[value];
            },
            index,
            settings.Duration)
        .SetEase(settings.Ease);
}