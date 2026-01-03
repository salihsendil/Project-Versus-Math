using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LobbyUIController : MonoBehaviour
{
    //References
    [Inject] private TournamentInstaller tournamentInstaller;

    [Header("Entries")]
    [SerializeField] private List<PlayerLobbyEntry> entries = new();

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        SetLobbyEntries(tournamentInstaller.Participants);
    }

    private void SetLobbyEntries(List<ParticipantData> participants)
    {
        for (int i = 0; i < participants.Count; i++)
        {
            entries[i].SetEntryActive(true);
            entries[i].SetText(participants[i].Name);
        }


        //tournamentinstaller turnuva kurar, eþleþmeleri ayarlar kullanýcýlarýn isimlerini random setler
        //roundmanager oyunu ayarlar, kullanýcýlarý bilir, bitme durumunu kontrol eder
        //playerstate roundmanager içinde olur duruma göre kontrol edilir.
    }

    public void OnStartButtonClicked()
    {

    }

    public void OnMainMenuButtonClicked()
    {

    }

}
