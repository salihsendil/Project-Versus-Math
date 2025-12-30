using System.Collections.Generic;
using UnityEngine;
using Zenject;

[System.Serializable]
public struct ParticipantData
{
    public string ParticipantName;

    public ParticipantData(string participantName)
    {
        ParticipantName = participantName;
    }
}

public class TournamentInstaller : MonoBehaviour
{
    //Data
    [Inject] private GameConfigSO gameConfig;
    [Inject] private ParticipantSO participantData;

    //List
    [SerializeField] private List<ParticipantData> participants = new();

    private void SetTournamentSize(int size)
    {
        gameConfig.TournamentSize = size;
    }

    private void SetParticipantsData()
    {
        while (participants.Count < gameConfig.TournamentSize)
        {
            int random = Random.Range(0, participantData.DefaultNames.Count);
            ParticipantData participant = new(participantData.DefaultNames[random]);

            if (!participants.Contains(participant))
            {
                participants.Add(participant);
            }
        }
    }
}
