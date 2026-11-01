using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public sealed class BackgroundAnimation : BaseAnimation<BackgroundAnimationSettings>
{
    private readonly Image _image;

    private readonly Sprite[] _sprites;

    public BackgroundAnimation(Image image, Sprite[] sprites) => (_image, _sprites) = (image, sprites);

    public override void InitializeSequence(BackgroundAnimationSettings settings) =>
        Sequence.Join(CreateSpriteTween(settings.SpriteIndex, settings.SpriteIndexTweenSettings));

    private Tween CreateSpriteTween(int index, TweenSettings settings) =>
        DOTween.To(
            () => 0,
            value => _image.sprite = _sprites[value],
            index,
            settings.Duration)
        .SetEase(settings.Ease);
}