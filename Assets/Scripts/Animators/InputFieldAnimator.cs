using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(InputFieldStateHandler))]
public sealed class InputFieldAnimator : StateAnimator<
    InputFieldAnimatorSettings,
    InputFieldState,
    InputFieldAnimationSettings,
    InputFieldStateHandler,
    InputFieldAnimation>
{
    [SerializeField] private TextMeshProUGUI _placeholderTextTMP;
    [SerializeField] private TextMeshProUGUI _labelTextTMP;

    private RectTransform _rectTransform;
    private Image _image;

    protected override void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        base.Awake();
    }

    protected override InputFieldAnimation CreateAnimation() =>
        new(
            _rectTransform,
            _image,
            Settings.Sprites,
            _placeholderTextTMP,
            _labelTextTMP);

    protected override InputFieldAnimationSettings GetStateAnimationSettings(InputFieldState state) =>
        state switch
        {
            InputFieldState.Normal => Settings.NormalAnimationSettings,
            InputFieldState.Hover => Settings.HoverAnimationSettings,
            InputFieldState.Pressed => Settings.PressedAnimationSettings,
            InputFieldState.Change => Settings.ChangeAnimationSettings,
            _ => Settings.NormalAnimationSettings
        };
}