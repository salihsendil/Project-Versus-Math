using TMPro;
using UnityEngine;
using Zenject;

public class ResultUIView : MonoBehaviour
{
    [Inject] private SignalBus signalBus;

    [SerializeField] private TMP_Text winnerName;

    public void SetVisibility(bool isVisible)
    {
        if (gameObject.activeSelf == isVisible) return;
        gameObject.SetActive(isVisible);
    }

    public void SetScreen(string winner)
    {
        winnerName.text = winner;
    }

    public void OnNextRoundButtonPressed()
    {
        SetVisibility(false);
        signalBus.Fire(new NextRoundRequestSignal());
    }
}
