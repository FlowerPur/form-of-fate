using System;
using UnityEngine;
using DG.Tweening;

[Serializable]
public struct TweenSettings
{
    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public Ease Ease { get; private set; }
}