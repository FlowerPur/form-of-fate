using UnityEngine;

[CreateAssetMenu(
    fileName = "CustomSliderAnimatorSettings",
    menuName = "AnimatorSettings/CustomSliderAnimatorSettings")]
public sealed class CustomSliderAnimatorSettings : StateAnimatorSettings
{
    [field: SerializeField] public Sprite[] LabelSprites { get; private set; }

    [field: SerializeField] public CustomSliderAnimationSettings NormalAnimationSettings { get; private set; }
    [field: SerializeField] public CustomSliderAnimationSettings ChangeAnimationSettings { get; private set; }
}