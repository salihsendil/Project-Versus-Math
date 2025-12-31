using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LobbyUIController : MonoBehaviour
{
    //References
    [Inject] private SceneService sceneService;
    [Inject] private TournamentInstaller tournamentInstaller;
    
    //Data
    [SerializeField] private List<LobbyPlayerItem> players = new();

    private void Awake()
    {
        foreach (var player in players)
        {
            player.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        SetParticipants(tournamentInstaller.Participants);
    }

    private void SetParticipants(List<ParticipantData> participants)
    {
        if (participants == null || participants.Count <= 0) return;

        for (int i = 0; i < participants.Count; i++)
        {
            players[i].gameObject.SetActive(true);
            players[i].SetPlayersName(participants[i].ParticipantName);
        }
    }

    public void LoadScene(ScenesEnum scenesEnum)
    {
        sceneService.LoadSceneWithLoading(scenesEnum);
    }
}
