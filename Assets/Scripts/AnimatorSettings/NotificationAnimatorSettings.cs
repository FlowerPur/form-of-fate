using UnityEngine;

[CreateAssetMenu(
    fileName = "NotificationAnimatorSettings",
    menuName = "AnimatorSettings/NotificationAnimatorSettings")]
public sealed class NotificationAnimatorSettings : StateAnimatorSettings
{
    [field: SerializeField] public NotificationAnimationSettings CloseAnimationSettings { get; private set; }
    [field: SerializeField] public NotificationAnimationSettings OpenAnimationSettings { get; private set; }
}