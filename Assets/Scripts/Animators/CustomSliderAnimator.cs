using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CustomSlider))]
public sealed class CustomSliderAnimator : StateAnimator<
    CustomSliderAnimatorSettings,
    CustomSliderState,
    CustomSliderAnimationSettings,
    CustomSlider,
    CustomSliderAnimation>
{
    [SerializeField] private GameObject _label;
    [SerializeField] private TextMeshProUGUI _labelTextTMP;

    private RectTransform _labelRectTransform;
    private Image _labelBackgroundImage;

    protected override void Awake()
    {
        _labelRectTransform = _label.GetComponent<RectTransform>();
        _labelBackgroundImage = _label.GetComponent<Image>();

        base.Awake();
    }

    protected override CustomSliderAnimation CreateAnimation() =>
        new(
            _labelRectTransform,
            _labelBackgroundImage,
            Settings.LabelSprites,
            _labelTextTMP);

    protected override CustomSliderAnimationSettings GetStateAnimationSettings(CustomSliderState state) =>
        state switch
        {
            CustomSliderState.Normal => Settings.NormalAnimationSettings,
            CustomSliderState.Change => Settings.ChangeAnimationSettings,
            _ => Settings.NormalAnimationSettings
        };
}