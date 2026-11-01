using UnityEngine;

[CreateAssetMenu(
    fileName = "SliderAnimatorSettings",
    menuName = "AnimatorSettings/SliderAnimatorSettings")]
public sealed class SliderAnimatorSettings : StateAnimatorSettings
{
    [field: SerializeField] public Sprite[] Sprites { get; private set; }
    [field: SerializeField] public Sprite[] FillSprites { get; private set; }

    [field: SerializeField] public SliderAnimationSettings NormalAnimationSettings { get; private set; }
    [field: SerializeField] public SliderAnimationSettings HoverAnimationSettings { get; private set; }
    [field: SerializeField] public SliderAnimationSettings PressedAnimationSettings { get; private set; }
}