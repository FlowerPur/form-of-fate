using System;
using UnityEngine;

[Serializable]
public struct IconButtonAnimationSettings : IButtonAnimationSettings
{
    [field: SerializeField] public float SizeOffset { get; private set; }
    [field: SerializeField] public int SpriteIndex { get; private set; }

    [field: SerializeField] public TweenSettings SizeOffsetTweenSettings { get; private set; }
    [field: SerializeField] public TweenSettings SpriteIndexTweenSettings { get; private set; }

    [field: SerializeField] public float IconSizeOffset { get; private set; }

    [field: SerializeField] public TweenSettings IconSizeOffsetTweenSettings { get; private set; }
}