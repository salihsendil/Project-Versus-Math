using TMPro;
using UnityEngine;
using Zenject;

public class PreparationUIView : MonoBehaviour
{
    [Inject] private SignalBus signalBus;
    [Inject] private TournamentInstaller tournamentInstaller;
    [Inject] private SoundDataSO soundData;
    [Inject] private AudioService audioService;

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
        audioService.PlaySfx(soundData.roundStart);
        playerOneText.text = matchup.playerOne.Name;
        playerTwoText.text = matchup.playerTwo.Name;
    }

    public void OnStartButtonPressed()
    {
        audioService.PlaySfx(soundData.buttonClick);
        gameObject.SetActive(false);
        signalBus.Fire(new StartRoundRequestSignal(playerOneText.text, playerTwoText.text));
    }
}
