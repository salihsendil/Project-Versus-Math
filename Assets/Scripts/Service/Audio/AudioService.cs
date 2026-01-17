using UnityEngine;
using Zenject;

public class AudioService : MonoBehaviour
{
    [Inject] private SoundDataSO soundData;
    [Inject] private SignalBus signalBus;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private void OnEnable()
    {
        signalBus.Subscribe<OnSoundOptionChangedSignal>(OnSoundPreferencesChanged);
    }

    private void OnDisable()
    {
        signalBus?.TryUnsubscribe<OnSoundOptionChangedSignal>(OnSoundPreferencesChanged);
    }

    private void Start()
    {
        UpdateVolumes();

        if (soundData.isSoundOn && soundData.backgroundMusic != null)
        {
            PlayMusic(soundData.backgroundMusic);
        }
        else if (soundData.backgroundMusic != null)
        {
            musicSource.clip = soundData.backgroundMusic;
            musicSource.loop = true;
        }
    }

    public void PlaySfx(AudioClip audioClip)
    {
        if (!soundData.isSoundOn || audioClip == null) return;

        sfxSource.PlayOneShot(audioClip);
    }

    public void PlayMusic(AudioClip audioClip)
    {
        if (audioClip == null) return;
        if (musicSource.clip == audioClip && musicSource.isPlaying) return;

        musicSource.clip = audioClip;
        musicSource.loop = true;

        musicSource.volume = soundData.isSoundOn ? soundData.defaultMusicVolume : 0f;
        musicSource.Play();

        if (!soundData.isSoundOn)
        {
            musicSource.Pause();
        }
    }

    private void UpdateVolumes()
    {
        musicSource.volume = soundData.isSoundOn ? soundData.defaultMusicVolume : 0f;
        sfxSource.volume = soundData.isSoundOn ? soundData.defaultSfxVolume : 0f;
    }

    private void OnSoundPreferencesChanged(OnSoundOptionChangedSignal signal)
    {
        soundData.isSoundOn = signal.IsOn;
        UpdateVolumes();

        if (soundData.isSoundOn)
        {
            if (musicSource.clip == null)
            {
                PlayMusic(soundData.backgroundMusic);
            }
            else
            {
                musicSource.UnPause();
                if (!musicSource.isPlaying) musicSource.Play();
            }
        }
        else
        {
            musicSource.Pause();
            sfxSource.Stop();
        }
    }
}