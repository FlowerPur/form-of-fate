public interface IButtonAnimationSettings : IAnimationSettings
{
    public float SizeOffset { get; }
    public int SpriteIndex { get; }

    public TweenSettings SizeOffsetTweenSettings { get; }
    public TweenSettings SpriteIndexTweenSettings { get; }
}