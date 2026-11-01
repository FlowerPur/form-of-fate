using System;
using UnityEngine;

[Serializable]
public struct NotificationAnimationSettings : IAnimationSettings
{
    [field: SerializeField] public float ContentSizeOffset { get; private set; }

    [field: SerializeField] public TweenSettings ContentSizeOffsetTweenSettings { get; private set; }

    [field: SerializeField] public float DarkeningProgress { get; private set; }

    [field: SerializeField] public TweenSettings DarkeningProgressTweenSettings { get; private set; }
}