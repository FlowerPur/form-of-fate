using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public sealed class InterfaceScaler : MonoBehaviour
{
    private readonly Vector2 _minimumResolution = new(768, 576);
    private readonly Vector2 _maximumResolution = new(384, 216);

    private CanvasScaler _canvasScaler;

    private void Awake() => _canvasScaler = GetComponent<CanvasScaler>();

    private void OnEnable()
    {
        SettingsStorage.SettingsChanged += UpdateScale;

        UpdateScale(SettingsStorage.Load());
    }

    private void OnDisable() => SettingsStorage.SettingsChanged -= UpdateScale;

    private void UpdateScale(SettingsData settings) => _canvasScaler.referenceResolution = GetScale(settings.InterfaceSize);

    private Vector2 GetScale(int size) => Vector2.Lerp(_minimumResolution, _maximumResolution, (float)size / 100);
}