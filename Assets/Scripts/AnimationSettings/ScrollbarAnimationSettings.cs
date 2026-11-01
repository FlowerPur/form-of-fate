using System;
using UnityEngine;

[Serializable]
public struct ScrollbarAnimationSettings : IAnimationSettings
{
    [field: SerializeField] public float SizeOffset { get; private set; }
    [field: SerializeField] public int SpriteIndex { get; private set; }

    [field: SerializeField] public TweenSettings SizeOffsetTweenSettings { get; private set; }
    [field: SerializeField] public TweenSettings SpriteIndexTweenSettings { get; private set; }

    [field: SerializeField] public int HandleSpriteIndex { get; private set; }

    [field: SerializeField] public TweenSettings HandleSpriteIndexTweenSettings { get; private set; }
}