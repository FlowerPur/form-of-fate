using UnityEngine;

[RequireComponent(typeof(Notification))]
public sealed class NotificationAnimator : StateAnimator<
    NotificationAnimatorSettings,
    NotificationState,
    NotificationAnimationSettings,
    Notification,
    NotificationAnimation>
{
    [SerializeField] private RectTransform _contentRectTransform;
    [SerializeField] private PixelEraser _darkening;

    protected override NotificationAnimation CreateAnimation() => new(_contentRectTransform, _darkening);

    protected override NotificationAnimationSettings GetStateAnimationSettings(NotificationState state) =>
        state switch
        {
            NotificationState.Close => Settings.CloseAnimationSettings,
            NotificationState.Open => Settings.OpenAnimationSettings,
            _ => Settings.CloseAnimationSettings
        };
}