using System;
using UnityEngine;

[Serializable]
public struct SliderAnimationSettings : IAnimationSettings
{
    [field: SerializeField] public float SizeOffset { get; private set; }
    [field: SerializeField] public int SpriteIndex { get; private set; }

    [field: SerializeField] public TweenSettings SizeOffsetTweenSettings { get; private set; }
    [field: SerializeField] public TweenSettings SpriteIndexTweenSettings { get; private set; }

    [field: SerializeField] public int FillSpriteIndex { get; private set; }
    [field: SerializeField] public TweenSettings FillSpriteIndexTweenSettings { get; private set; }
}