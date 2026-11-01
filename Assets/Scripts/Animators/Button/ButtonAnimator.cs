using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(ButtonStateHandler))]
public abstract class ButtonAnimator<TAnimationSettings, TAnimation, TSettings>
    : StateAnimator<
        TSettings,
        ButtonState,
        TAnimationSettings,
        ButtonStateHandler,
        TAnimation>
    where TAnimationSettings : IButtonAnimationSettings
    where TAnimation : ButtonAnimation<TAnimationSettings>
    where TSettings : ButtonAnimatorSettings<TAnimationSettings>
{
    protected RectTransform RectTransform { get; private set; }
    protected Image Image { get; private set; }

    protected sealed override void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        Image = GetComponent<Image>();

        base.Awake();
    }

    protected sealed override TAnimationSettings GetStateAnimationSettings(ButtonState state) =>
        state switch
        {
            ButtonState.Disabled => Settings.DisabledAnimationSettings,
            ButtonState.Normal => Settings.NormalAnimationSettings,
            ButtonState.Hover => Settings.HoverAnimationSettings,
            ButtonState.Pressed => Settings.PressedAnimationSettings,
            _ => Settings.NormalAnimationSettings
        };
}