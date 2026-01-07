using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResultUIView : MonoBehaviour
{
    [Inject] private SignalBus signalBus;
    [Inject] private SceneService sceneService;

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

    public void MainMenuButtonPressed()
    {
        SetVisibility(false);
        sceneService.LoadSceneWithLoading(ScenesEnum.MainMenu);
    }
}
