using System;
using UnityEngine;

[Serializable]
public struct InputFieldAnimationSettings : IAnimationSettings
{
    [field: SerializeField] public float SizeOffset { get; private set; }
    [field: SerializeField] public int SpriteIndex { get; private set; }

    [field: SerializeField] public TweenSettings SizeOffsetTweenSettings { get; private set; }
    [field: SerializeField] public TweenSettings SpriteIndexTweenSettings { get; private set; }

    [field: SerializeField] public float PlaceholderTextFontSizeMaxOffset { get; private set; }

    [field: SerializeField] public TweenSettings PlaceholderTextFontSizeMaxOffsetTweenSettings { get; private set; }

    [field: SerializeField] public float LabelTextFontSizeMaxOffset { get; private set; }

    [field: SerializeField] public TweenSettings LabelTextFontSizeMaxOffsetTweenSettings { get; private set; }
}