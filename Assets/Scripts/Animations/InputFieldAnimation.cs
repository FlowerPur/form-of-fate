using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public sealed class InputFieldAnimation : BaseAnimation<InputFieldAnimationSettings>
{
    private readonly RectTransform _rectTransform;
    private readonly Image _image;
    private readonly TextMeshProUGUI _placeholderTextTMP;
    private readonly TextMeshProUGUI _labelTextTMP;

    private readonly Sprite[] _sprites;

    private readonly Vector2 _originalOffsetMin;
    private readonly Vector2 _originalOffsetMax;

    private readonly float _originalPlaceholderTextFontSizeMax;
    private readonly float _originalLabelTextFontSizeMax;

    private int _spriteIndex;

    public InputFieldAnimation(
        RectTransform rectTransform,
        Image image,
        Sprite[] sprites,
        TextMeshProUGUI placeholderTextTMP,
        TextMeshProUGUI labelTextTMP)
    {
        _rectTransform = rectTransform;
        _image = image;
        _placeholderTextTMP = placeholderTextTMP;
        _labelTextTMP = labelTextTMP;

        _sprites = sprites;

        _originalOffsetMin = rectTransform.offsetMin;
        _originalOffsetMax = rectTransform.offsetMax;

        _originalPlaceholderTextFontSizeMax = placeholderTextTMP.fontSizeMax;
        _originalLabelTextFontSizeMax = labelTextTMP.fontSizeMax;
    }

    public override void InitializeSequence(InputFieldAnimationSettings settings)
    {
        Sequence.Join(CreateSizeTween(settings.SizeOffset, settings.SizeOffsetTweenSettings));
        Sequence.Join(CreateSpriteTween(settings.SpriteIndex, settings.SpriteIndexTweenSettings));
        Sequence.Join(CreatePlaceholderTextFontSizeMaxTween(settings.PlaceholderTextFontSizeMaxOffset, settings.PlaceholderTextFontSizeMaxOffsetTweenSettings));
        Sequence.Join(CreateLabelTextFontSizeMaxTween(settings.LabelTextFontSizeMaxOffset, settings.LabelTextFontSizeMaxOffsetTweenSettings));
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

    private Tween CreatePlaceholderTextFontSizeMaxTween(float offset, TweenSettings settings) =>
        DOTween.To(
            () => _placeholderTextTMP.fontSizeMax,
            value => _placeholderTextTMP.fontSizeMax = value,
            _originalPlaceholderTextFontSizeMax + offset,
            settings.Duration)
        .SetEase(settings.Ease);

    private Tween CreateLabelTextFontSizeMaxTween(float offset, TweenSettings settings) =>
        DOTween.To(
            () => _labelTextTMP.fontSizeMax,
            value => _labelTextTMP.fontSizeMax = value,
            _originalLabelTextFontSizeMax + offset,
            settings.Duration)
        .SetEase(settings.Ease);
}