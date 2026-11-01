using System;
using UnityEngine;

[Serializable]
public struct MenuAnimationSettings : IAnimationSettings
{
    [field: SerializeField] public bool IsDarkeningFull { get; private set; }

    [field: SerializeField] public TweenSettings DarkeningTweenSettings { get; private set; }
}