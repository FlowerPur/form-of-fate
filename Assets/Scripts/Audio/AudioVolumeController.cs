using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public sealed class AudioVolumeController : MonoBehaviour
{
    [SerializeField] private AudioVolumeType _type;

    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void OnEnable()
    {
        SettingsStorage.SettingsChanged += UpdateVolume;

        UpdateVolume(SettingsStorage.Load());
    }

    private void OnDisable() => SettingsStorage.SettingsChanged -= UpdateVolume;

    private void UpdateVolume(SettingsData settings) =>
        _audioSource.volume = (settings.OverallVolume / 100f) * GetSpecificVolume(settings);

    private float GetSpecificVolume(SettingsData settings) =>
        _type switch
        {
            AudioVolumeType.Sound => settings.SoundVolume / 100f,
            AudioVolumeType.Music => settings.MusicVolume / 100f,
            _ => 1f
        };
}
