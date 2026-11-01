using UnityEngine;

[CreateAssetMenu(
    fileName = "SelectorAnimatorSettings",
    menuName = "AnimatorSettings/SelectorAnimatorSettings")]
public sealed class SelectorAnimatorSettings : StateAnimatorSettings
{
    [field: SerializeField] public Sprite[] LabelSprites { get; private set; }

    [field: SerializeField] public SelectorAnimationSettings NormalAnimationSettings { get; private set; }
    [field: SerializeField] public SelectorAnimationSettings ChangeAnimationSettings { get; private set; }
}