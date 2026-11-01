using UnityEngine;

[CreateAssetMenu(
    fileName = "ScrollbarAnimatorSettings",
    menuName = "AnimatorSettings/ScrollbarAnimatorSettings")]
public sealed class ScrollbarAnimatorSettings : StateAnimatorSettings
{
    [field: SerializeField] public Sprite[] Sprites { get; private set; }
    [field: SerializeField] public Sprite[] HandleSprites { get; private set; }

    [field: SerializeField] public ScrollbarAnimationSettings NormalAnimationSettings { get; private set; }
    [field: SerializeField] public ScrollbarAnimationSettings HoverAnimationSettings { get; private set; }
    [field: SerializeField] public ScrollbarAnimationSettings PressedAnimationSettings { get; private set; }
}