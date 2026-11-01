using UnityEngine;

[CreateAssetMenu(
    fileName = "MenuAnimatorSettings",
    menuName = "AnimatorSettings/MenuAnimatorSettings")]
public sealed class MenuAnimatorSettings : StateAnimatorSettings
{
    [field: SerializeField] public MenuAnimationSettings CloseAnimationSettings { get; private set; }
    [field: SerializeField] public MenuAnimationSettings OpenAnimationSettings { get; private set; }
}