using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[Serializable]
public struct ParticipantData
{
    public string ParticipantName;

    public ParticipantData(string participantName)
    {
        ParticipantName = participantName;
    }
}

public class TournamentInstaller : MonoBehaviour, IInitializable, IDisposable
{
    //Data
    [Inject] private SignalBus signalBus;
    [Inject] private GameConfigSO gameConfig;
    [Inject] private ParticipantSO participantData;

    //List
    private List<ParticipantData> participants = new();


    public List<ParticipantData> Participants => participants;

    public void Initialize()
    {
        signalBus.Subscribe<LoadLobbyRequest>(SetParticipantsData);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<LoadLobbyRequest>(SetParticipantsData);
    }

    private void SetParticipantsData()
    {
        participants.Clear();

        while (participants.Count < gameConfig.TournamentSize)
        {
            int random = UnityEngine.Random.Range(0, participantData.DefaultNames.Count);
            ParticipantData participant = new(participantData.DefaultNames[random]);

            if (!participants.Contains(participant))
            {
                participants.Add(participant);
            }
        }
    }
}
