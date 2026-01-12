using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(RectTransform))]
public class OperationSelectButton : MonoBehaviour
{
    [Inject] private SignalBus signalBus;

    [SerializeField] private Button button;
    [SerializeField] private Image image;
    private RectTransform rectTransform;

    [SerializeField] private OperationTypes operation;
    private bool isAllowed;

    private void Awake()
    {
        TryGetComponent(out rectTransform);
    }

    public void SetButtonVisual(bool isAllowed)
    {
        this.isAllowed = isAllowed;
        image.color = isAllowed ? Color.green : Color.red;
        rectTransform.DOComplete();
        rectTransform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f, 10, 1);
    }

    public void OnButtonClicked()
    {
        isAllowed = !isAllowed;
        SetButtonVisual(isAllowed);
        signalBus.Fire(new OperationsSelectionChangedSignal(operation, isAllowed));
    }

}