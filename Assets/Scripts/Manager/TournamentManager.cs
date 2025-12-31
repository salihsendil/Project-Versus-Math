using System.Collections.Generic;
using UnityEngine;
using Zenject;

public struct MatchUpData
{
    public ParticipantData p1;
    public ParticipantData p2;
}

public class TournamentManager : MonoBehaviour
{
    //References
    [Inject] private SignalBus signalBus;
    [Inject] private GameConfigSO gameConfig;
    [Inject] private ParticipantSO participantData;

    //Data
    private Queue<MatchUpData> matchUps = new();

    private void AnalyzeParticipantsCount()
    {

    }


    private void InitializeTournament(List<ParticipantData> participants)
    {

    }

    private void CreateNextRoundMathcups()
    {

    }

}
