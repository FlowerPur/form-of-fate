using UnityEngine;

[CreateAssetMenu(
    fileName = "BackgroundAnimatorSettings",
    menuName = "AnimatorSettings/BackgroundAnimatorSettings")]
public sealed class BackgroundAnimatorSettings : SimpleAnimatorSettings<BackgroundAnimationSettings>
{
    [field: SerializeField] public Sprite[] Sprites { get; private set; }
}