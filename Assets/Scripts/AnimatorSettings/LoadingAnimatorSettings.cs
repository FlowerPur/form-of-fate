using UnityEngine;

[CreateAssetMenu(
    fileName = "LoadingAnimatorSettings",
    menuName = "AnimatorSettings/LoadingAnimatorSettings")]
public sealed class LoadingAnimatorSettings : SimpleAnimatorSettings<LoadingAnimationSettings>
{
    [field: SerializeField] public Sprite[] Sprites { get; private set; }
}