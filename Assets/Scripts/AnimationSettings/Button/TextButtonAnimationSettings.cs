using System;
using UnityEngine;

[Serializable]
public struct TextButtonAnimationSettings : IButtonAnimationSettings
{
    [field: SerializeField] public float SizeOffset { get; private set; }
    [field: SerializeField] public int SpriteIndex { get; private set; }

    [field: SerializeField] public TweenSettings SizeOffsetTweenSettings { get; private set; }
    [field: SerializeField] public TweenSettings SpriteIndexTweenSettings { get; private set; }

    [field: SerializeField] public float TextFontSizeMaxOffset { get; private set; }

    [field: SerializeField] public TweenSettings TextFontSizeMaxOffsetTweenSettings { get; private set; }
}