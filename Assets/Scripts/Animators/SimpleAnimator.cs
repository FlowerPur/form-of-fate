public abstract class SimpleAnimator<TAnimationSettings, TSettings, TAnimation>
    : BaseAnimator<TSettings, TAnimationSettings, TAnimation>
    where TAnimationSettings : IAnimationSettings
    where TSettings : SimpleAnimatorSettings<TAnimationSettings>
    where TAnimation : BaseAnimation<TAnimationSettings>
{
}