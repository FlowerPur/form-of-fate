using UnityEngine;

public sealed class MenuAnimator : StateAnimator<
    MenuAnimatorSettings,
    MenuState,
    MenuAnimationSettings,
    Menu,
    MenuAnimation>
{
    [SerializeField] private PixelEraser _darkening;

    protected override MenuAnimation CreateAnimation() => new(_darkening);

    protected override MenuAnimationSettings GetStateAnimationSettings(MenuState state) =>
        state switch
        {
            MenuState.Close => Settings.CloseAnimationSettings,
            MenuState.Open => Settings.OpenAnimationSettings,
            _ => Settings.CloseAnimationSettings
        };
}