using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OperationSelectButton : MonoBehaviour
{
    [Inject] private SignalBus signalBus;
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private OperationTypes operation;
    private bool isAllowed;

    public void SetButtonImage(bool isAllowed)
    {
        this.isAllowed = isAllowed;
        image.color = isAllowed ? Color.green : Color.red;
    }

    public void OnButtonClicked()
    {
        isAllowed = !isAllowed;
        SetButtonImage(isAllowed);
        signalBus.Fire(new OperationsSelectionChangedSignal(operation, isAllowed));
    }

}