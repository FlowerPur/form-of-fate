using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioVolumeController))]
public sealed class Music : MonoBehaviour
{
    [SerializeField] private AudioClip[] _regularAudioClips;
    [SerializeField] private AudioClip[] _strangeAudioClips;
    [SerializeField] private AudioClip[] _scaryAudioClips;
    [SerializeField] private AudioClip[] _sadAudioClips;
    [SerializeField] private AudioClip[] _combatAudioClips;

    [SerializeField] private float _fadeDuration = 1f;

    private AudioSource _audioSource;
    private Coroutine _changeMusicCoroutine;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void Start() => Play(MusicType.Regular);

    public void Play(MusicType type)
    {
        AudioClip newClip = GetTypeMusicClip(type);

        if (_changeMusicCoroutine != null)
            StopCoroutine(_changeMusicCoroutine);

        _changeMusicCoroutine = StartCoroutine(ChangeMusic(newClip));
    }

    private IEnumerator ChangeMusic(AudioClip newClip)
    {
        float targetVolume = _audioSource.volume;

        yield return FadeVolume(_audioSource.volume, 0f);

        _audioSource.clip = newClip;
        _audioSource.Play();

        yield return FadeVolume(0f, targetVolume);

        _changeMusicCoroutine = null;
    }

    private IEnumerator FadeVolume(float from, float to)
    {
        float time = 0f;

        while (time < _fadeDuration)
        {
            time += Time.deltaTime;

            float progress = Mathf.Clamp01(time / _fadeDuration);

            progress = Mathf.SmoothStep(0f, 1f, progress);

            _audioSource.volume = Mathf.Lerp(from, to, progress);

            yield return null;
        }

        _audioSource.volume = to;
    }

    private AudioClip GetTypeMusicClip(MusicType type) =>
        type switch
        {
            MusicType.Regular => GetRandomClip(_regularAudioClips),
            MusicType.Strange => GetRandomClip(_strangeAudioClips),
            MusicType.Scary => GetRandomClip(_scaryAudioClips),
            MusicType.Sad => GetRandomClip(_sadAudioClips),
            MusicType.Combat => GetRandomClip(_combatAudioClips),
            _ => GetRandomClip(_regularAudioClips)
        };

    private AudioClip GetRandomClip(AudioClip[] clips) => clips[Random.Range(0, clips.Length)];
}
