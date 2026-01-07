using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PreparationUIView : MonoBehaviour
{
    [Inject] private SignalBus signalBus;
    [Inject] private TournamentInstaller tournamentInstaller;

    [SerializeField] private TMP_Text playerOneText;
    [SerializeField] private TMP_Text playerTwoText;

    private void OnEnable()
    {
        SetScreen(tournamentInstaller.ShowNextMatch());
    }

    public void SetVisibility(bool isVisible)
    {
        if (gameObject.activeSelf == isVisible) return;
        gameObject.SetActive(isVisible);
    }

    private void SetScreen(MatchupData matchup)
    {
        playerOneText.text = matchup.playerOne.Name;
        playerTwoText.text = matchup.playerTwo.Name;
    }

    public void OnStartButtonPressed()
    {
        gameObject.SetActive(false);
        signalBus.Fire(new StartRoundRequestSignal(playerOneText.text, playerTwoText.text));
    }
}
