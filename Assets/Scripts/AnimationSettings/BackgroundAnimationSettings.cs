using System;
using UnityEngine;

[Serializable]
public struct BackgroundAnimationSettings : IAnimationSettings
{
    [field: SerializeField] public int SpriteIndex { get; private set; }

    [field: SerializeField] public TweenSettings SpriteIndexTweenSettings { get; private set; }
}