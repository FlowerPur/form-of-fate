using UnityEngine;

public abstract class ButtonAnimatorSettings<TAnimationSettings>
    : StateAnimatorSettings
    where TAnimationSettings : IButtonAnimationSettings
{
    [field: SerializeField] public Sprite[] Sprites { get; private set; }

    [field: SerializeField] public TAnimationSettings DisabledAnimationSettings { get; private set; }
    [field: SerializeField] public TAnimationSettings NormalAnimationSettings { get; private set; }
    [field: SerializeField] public TAnimationSettings HoverAnimationSettings { get; private set; }
    [field: SerializeField] public TAnimationSettings PressedAnimationSettings { get; private set; }
}