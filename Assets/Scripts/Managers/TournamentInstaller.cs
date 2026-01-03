using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;

[Serializable]
public struct ParticipantData
{
    public string Name;

    public ParticipantData(string name)
    {
        Name = name;
    }
}

[Serializable]
public struct MatchupData
{
    public ParticipantData playerOne;
    public ParticipantData playerTwo;

    public MatchupData(ParticipantData playerOne, ParticipantData playerTwo)
    {
        this.playerOne = playerOne;
        this.playerTwo = playerTwo;
    }
}

public class TournamentInstaller : IInitializable, IDisposable
{
    //References
    [Inject] private SignalBus signalBus;
    [Inject] private GameConfigSO gameConfigSO;
    [Inject] private ParticipantSO participantSO;

    //Participants
    private List<ParticipantData> participants = new();
    //private List<ParticipantData> testParticipants = new();
    private List<ParticipantData> byeParticipants = new();


    //MatchUps
    private Queue<MatchupData> matchups = new();

    //Getters
    public List<ParticipantData> Participants => participants;
    public Queue<MatchupData> Matchups => matchups;


    public void Initialize()
    {
        signalBus.Subscribe<LobbySetupRequestedSignal>(SetParticipantsData);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<LobbySetupRequestedSignal>(SetParticipantsData);
    }

    private void SetParticipantsData()
    {
        participants.Clear();

        int tournamentSize = gameConfigSO.TournamentSize;

        Shuffle(participantSO.DefaultNames);

        for (int i = 0; i < tournamentSize; i++)
        {
            participants.Add(new ParticipantData(participantSO.DefaultNames[i]));
        }

        StartTournament();

    }

    private void StartTournament()
    {
        if (!IsPowerOfTwo(participants.Count))
        {
            int nextPower = NextPowerOfTwo(participants.Count);
            int byeParticipantsCount = nextPower - participants.Count;

            for (int i = 0; i < byeParticipantsCount; i++)
            {
                int random = UnityEngine.Random.Range(0, participants.Count);
                byeParticipants.Add(new ParticipantData(participants[i].Name));
                participants.RemoveAt(random);
            }
            Debug.Log($"Ön eleme oynamayacak oyuncu sayýsý {byeParticipantsCount}.");
        }

        CreateRoundMatchup(participants);
    }

    private void CreateRoundMatchup(List<ParticipantData> participants)
    {
        matchups.Clear();
        Debug.Log("Eþleþmeler: ");

        while (participants.Count > 0)
        {
            int randomP1 = UnityEngine.Random.Range(0, participants.Count);
            ParticipantData p1 = new(participants[randomP1].Name);
            participants.RemoveAt(randomP1);

            int randomP2 = UnityEngine.Random.Range(0, participants.Count);
            ParticipantData p2 = new(participants[randomP2].Name);
            participants.RemoveAt(randomP2);

            MatchupData matchup = new(p1, p2);
            matchups.Enqueue(matchup);

            Debug.Log($"Eþleþme: {matchup.playerOne.Name} - {matchup.playerTwo.Name}.");
        }
    }


    #region Helpers
    public static void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    public bool IsPowerOfTwo(int n)
    {
        return n > 0 && (n & (n - 1)) == 0;
    }

    public int NextPowerOfTwo(int n)
    {
        int v = n;
        v--;
        v |= v >> 1;
        v |= v >> 2;
        v |= v >> 4;
        v |= v >> 8;
        v |= v >> 16;
        v++;
        return v;
    }
    #endregion
}
