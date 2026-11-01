using DG.Tweening;

public sealed class MenuAnimation : BaseAnimation<MenuAnimationSettings>
{
    private readonly PixelEraser _darkening;

    public MenuAnimation(PixelEraser darkening) => _darkening = darkening;

    public override void InitializeSequence(MenuAnimationSettings settings) =>
        Sequence.Join(CreateDarkeningTween(settings.IsDarkeningFull, settings.DarkeningTweenSettings));

    private Tween CreateDarkeningTween(bool isFull, TweenSettings settings) =>
        DOTween.To(
            () => _darkening.Progress,
            value => _darkening.Progress = value,
            isFull ? 0f : 1f,
            settings.Duration)
        .SetEase(settings.Ease);
}