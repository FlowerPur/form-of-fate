using System;
using UnityEngine;

[Serializable]
public struct CustomSliderAnimationSettings : IAnimationSettings
{
    [field: SerializeField] public float LabelSizeOffset { get; private set; }
    [field: SerializeField] public int LabelSpriteIndex { get; private set; }

    [field: SerializeField] public TweenSettings LabelSizeOffsetTweenSettings { get; private set; }
    [field: SerializeField] public TweenSettings LabelSpriteIndexTweenSettings { get; private set; }

    [field: SerializeField] public float LabelTextFontSizeMaxOffset { get; private set; }

    [field: SerializeField] public TweenSettings LabelTextFontSizeMaxOffsetTweenSettings { get; private set; }
}