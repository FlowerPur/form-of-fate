using System.Collections;
using UnityEngine.UI;

public sealed class BackgroundAnimator : SimpleAnimator<
    BackgroundAnimationSettings,
    BackgroundAnimatorSettings,
    BackgroundAnimation>
{
    private Image _image;

    protected override void Awake()
    {
        InitializeImageComponent();

        base.Awake();

        InitializeAnimation();
    }

    private void OnEnable() => StartCoroutine(StartLoopAnimation());

    protected override BackgroundAnimation CreateAnimation() => new(_image, Settings.Sprites);

    private void InitializeImageComponent() => _image = GetComponent<Image>();

    private void InitializeAnimation()
    {
        Animation.CreateSequence();
        Animation.InitializeSequence(Settings.AnimationSettings);
    }

    private IEnumerator StartLoopAnimation()
    {
        Animation.Play();

        while (true)
        {
            while (Progress < 1f)
                yield return null;

            Animation.Restart();

            yield return null;
        }
    }
}