using UnityEngine;

public sealed class IconButtonAnimator : ButtonAnimator<
    IconButtonAnimationSettings,
    IconButtonAnimation,
    IconButtonAnimatorSettings>
{
    [SerializeField] private RectTransform _iconRectTransform;

    protected override IconButtonAnimation CreateAnimation() =>
        new(
            RectTransform,
            Image,
            Settings.Sprites,
            _iconRectTransform);
}