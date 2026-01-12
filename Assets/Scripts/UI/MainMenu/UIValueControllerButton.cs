using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(RectTransform))]
public class UIValueControllerButton : MonoBehaviour
{
    [Inject] private SignalBus signalBus;

    private Button button;
    private RectTransform rectTransform;
    [SerializeField] private int buttonValue;
    [SerializeField] private UIButtonDataType buttonType;

    private void Awake()
    {
        TryGetComponent(out button);
        TryGetComponent(out rectTransform);
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }

    private void PlayButtonAnimation()
    {
        rectTransform.DOComplete();
        rectTransform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f) * -1, 0.2f, 10, 1);
    }

    public void OnButtonClick()
    {
        PlayButtonAnimation();
        signalBus.Fire(new OnUIValueChangesButtonSignal(buttonValue, buttonType));
    }
}
