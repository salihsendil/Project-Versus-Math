using UnityEngine;

[CreateAssetMenu(fileName = "New SoundDataSO", menuName = "Scriptable Objects/New SoundDataSO")]
public class SoundDataSO : ScriptableObject
{
    [Header("User Preferences")]
    public bool isSoundOn;
    public float defaultSfxVolume = 0.7f;
    public float defaultMusicVolume = 0.6f;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    [Header("Music")]
    public AudioClip backgroundMusic;
    
    [Header("Answers")]
    public AudioClip correctAnswer;
    public AudioClip wrongAnswer;

    [Header("Button")]
    public AudioClip buttonClick;
    public AudioClip buttonClickPositive;
    public AudioClip buttonClickNegative;

    [Header("Scene Transition")]
    public AudioClip loadLobby;
    public AudioClip roundStart;
    public AudioClip roundEnd;
    public AudioClip tournamentEnd;
}
