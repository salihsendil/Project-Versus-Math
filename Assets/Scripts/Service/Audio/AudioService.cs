using UnityEngine;

public class AudioService : MonoBehaviour
{
    //References
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;

    public void PlaySfx(AudioClip audioClip, float volume = 0.5f) => sfxSource.PlayOneShot(audioClip, volume);

    public void PlayMusic(AudioClip audioClip, float volume = 0.5f) => musicSource.PlayOneShot(audioClip, volume);
}
