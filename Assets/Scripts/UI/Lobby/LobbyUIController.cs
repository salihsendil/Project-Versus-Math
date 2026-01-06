using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LobbyUIController : MonoBehaviour
{
    //References
    [Inject] private TournamentInstaller tournamentInstaller;
    [Inject] private SignalBus signalBus;
    [Inject] private SceneService sceneService;

    [Header("Entries")]
    [SerializeField] private List<PlayerLobbyEntry> entries = new();

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        SetLobbyEntries(tournamentInstaller.Participants);
    }

    private void SetLobbyEntries(List<PlayerRoundData> participants)
    {
        for (int i = 0; i < participants.Count; i++)
        {
            entries[i].SetEntryActive(true);
            entries[i].SetText(participants[i].Name);
        }
    }

    public void OnStartButtonClicked()
    {
        signalBus.Fire(new TournamentSetupRequestedSignal());
        sceneService.LoadSceneWithLoading(ScenesEnum.Game);
    }

    public void OnMainMenuButtonClicked()
    {
        sceneService.LoadSceneWithLoading(ScenesEnum.MainMenu);
    }

}
