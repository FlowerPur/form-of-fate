using UnityEngine;

[CreateAssetMenu(
    fileName = "InputFieldAnimatorSettings",
    menuName = "AnimatorSettings/InputFieldAnimatorSettings")]
public sealed class InputFieldAnimatorSettings : StateAnimatorSettings
{
    [field: SerializeField] public Sprite[] Sprites { get; private set; }

    [field: SerializeField] public InputFieldAnimationSettings NormalAnimationSettings { get; private set; }
    [field: SerializeField] public InputFieldAnimationSettings HoverAnimationSettings { get; private set; }
    [field: SerializeField] public InputFieldAnimationSettings PressedAnimationSettings { get; private set; }
    [field: SerializeField] public InputFieldAnimationSettings ChangeAnimationSettings { get; private set; }
}