using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(RectTransform))]
public class SoundButtonHandler : MonoBehaviour
{
    [Inject] private SoundDataSO soundData;
    [Inject] private SignalBus signalBus;

    private Button button;
    private RectTransform rectTransform;

    private void Awake()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        SetButtonVisual();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);
        signalBus.Subscribe<OnSoundOptionChangedSignal>(OnExternalSoundChange);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClick);
        signalBus.Unsubscribe<OnSoundOptionChangedSignal>(OnExternalSoundChange);
    }

    private void OnButtonClick()
    {
        bool newState = !soundData.isSoundOn;
        rectTransform.DOComplete();
        rectTransform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f, 10, 1);
        signalBus.Fire(new OnSoundOptionChangedSignal(newState));
    }

    private void OnExternalSoundChange(OnSoundOptionChangedSignal signal)
    {
        SetButtonVisual();
    }

    private void SetButtonVisual()
    {
        if (button.image != null)
        {
            button.image.sprite = soundData.isSoundOn ? soundData.soundOnSprite : soundData.soundOffSprite;
        }
    }
}