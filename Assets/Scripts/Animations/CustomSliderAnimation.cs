using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public sealed class CustomSliderAnimation : BaseAnimation<CustomSliderAnimationSettings>
{
    private readonly RectTransform _labelRectTransform;
    private readonly Image _labelImage;
    private readonly TextMeshProUGUI _labelTextTMP;

    private readonly Sprite[] _labelSprites;

    private readonly Vector2 _originalLabelOffsetMin;
    private readonly Vector2 _originalLabelOffsetMax;

    private readonly float _originalLabelTextFontSizeMax;

    private int _labelSpriteIndex;

    public CustomSliderAnimation(
        RectTransform labelRectTransform,
        Image labelImage,
        Sprite[] labelSprites,
        TextMeshProUGUI labelTextTMP)
    {
        _labelRectTransform = labelRectTransform;
        _labelImage = labelImage;
        _labelTextTMP = labelTextTMP;

        _labelSprites = labelSprites;

        _originalLabelOffsetMin = labelRectTransform.offsetMin;
        _originalLabelOffsetMax = labelRectTransform.offsetMax;

        _originalLabelTextFontSizeMax = labelTextTMP.fontSizeMax;
    }

    public override void InitializeSequence(CustomSliderAnimationSettings settings)
    {
        Sequence.Join(CreateLabelSizeTween(settings.LabelSizeOffset, settings.LabelSizeOffsetTweenSettings));
        Sequence.Join(CreateLabelSpriteTween(settings.LabelSpriteIndex, settings.LabelSpriteIndexTweenSettings));
        Sequence.Join(CreateLabelTextFontSizeMaxTween(settings.LabelTextFontSizeMaxOffset, settings.LabelTextFontSizeMaxOffsetTweenSettings));
    }

    private Tween CreateLabelSizeTween(float offset, TweenSettings settings) =>
        DOTween.To(
            () => _originalLabelOffsetMin.x - _labelRectTransform.offsetMin.x,
            value =>
            {
                _labelRectTransform.offsetMin = _originalLabelOffsetMin - (Vector2.one * value);
                _labelRectTransform.offsetMax = _originalLabelOffsetMax + (Vector2.one * value);
            },
            offset,
            settings.Duration)
        .SetEase(settings.Ease);

    private Tween CreateLabelSpriteTween(int index, TweenSettings settings) =>
        DOTween.To(
            () => _labelSpriteIndex,
            value =>
            {
                _labelSpriteIndex = value;
                _labelImage.sprite = _labelSprites[value];
            },
            index,
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