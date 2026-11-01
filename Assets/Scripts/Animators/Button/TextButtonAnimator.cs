using UnityEngine;
using TMPro;

public sealed class TextButtonAnimator : ButtonAnimator<
    TextButtonAnimationSettings,
    TextButtonAnimation,
    TextButtonAnimatorSettings>
{
    [SerializeField] private TextMeshProUGUI _textTMP;

    protected override TextButtonAnimation CreateAnimation() =>
        new(
            RectTransform,
            Image,
            Settings.Sprites,
            _textTMP);
}