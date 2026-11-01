using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public sealed class TextButtonAnimation : ButtonAnimation<TextButtonAnimationSettings>
{
    private readonly TextMeshProUGUI _textTMP;

    private readonly float _originalTextFontSizeMax;

    public TextButtonAnimation(
        RectTransform rectTransform,
        Image image,
        Sprite[] sprites,
        TextMeshProUGUI textTMP)
        : base(rectTransform, image, sprites)
    {
        _textTMP = textTMP;

        _originalTextFontSizeMax = textTMP.fontSizeMax;
    }

    public override void InitializeSequence(TextButtonAnimationSettings settings)
    {
        base.InitializeSequence(settings);

        Sequence.Join(CreateTextFontSizeMaxTween(settings.TextFontSizeMaxOffset, settings.TextFontSizeMaxOffsetTweenSettings));
    }

    private Tween CreateTextFontSizeMaxTween(float offset, TweenSettings settings) =>
        DOTween.To(
            () => _textTMP.fontSizeMax,
            value => _textTMP.fontSizeMax = value,
            _originalTextFontSizeMax + offset,
            settings.Duration)
        .SetEase(settings.Ease);
}