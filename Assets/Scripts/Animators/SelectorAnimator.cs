using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Selector))]
public sealed class SelectorAnimator : StateAnimator<
    SelectorAnimatorSettings,
    SelectorState,
    SelectorAnimationSettings,
    Selector,
    SelectorAnimation>
{
    [SerializeField] private GameObject _label;
    [SerializeField] private TextMeshProUGUI _labelTextTMP;

    private RectTransform _labelRectTransform;
    private Image _labelImage;

    protected override void Awake()
    {
        _labelRectTransform = _label.GetComponent<RectTransform>();
        _labelImage = _label.GetComponent<Image>();

        base.Awake();
    }

    protected override SelectorAnimation CreateAnimation() =>
        new(
            _labelRectTransform,
            _labelImage,
            Settings.LabelSprites,
            _labelTextTMP);

    protected override SelectorAnimationSettings GetStateAnimationSettings(SelectorState state) =>
        state switch
        {
            SelectorState.Normal => Settings.NormalAnimationSettings,
            SelectorState.Change => Settings.ChangeAnimationSettings,
            _ => Settings.NormalAnimationSettings
        };
}