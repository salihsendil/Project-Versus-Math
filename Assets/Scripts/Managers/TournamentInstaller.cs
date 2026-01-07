using System.Collections.Generic;
using System;
using Zenject;

[Serializable]
public struct MatchupData
{
    public PlayerRoundData playerOne;
    public PlayerRoundData playerTwo;
}

public class TournamentInstaller : IInitializable, IDisposable
{
    //References
    [Inject] private SignalBus signalBus;
    [Inject] private GameConfigSO gameConfigSO;
    [Inject] private ParticipantSO participantSO;

    //Participants
    private List<PlayerRoundData> participants = new();
    private List<PlayerRoundData> byeParticipants = new();

    //MatchUps
    private Queue<MatchupData> matchups = new();

    //Getters
    public List<PlayerRoundData> Participants => participants;


    public void Initialize()
    {
        signalBus.Subscribe<LobbySetupRequestedSignal>(SetParticipantsData);
        signalBus.Subscribe<TournamentSetupRequestedSignal>(StartTournament);
        signalBus.Subscribe<RoundCompletedSignal>(AddPlayerToNextRound);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<LobbySetupRequestedSignal>(SetParticipantsData);
        signalBus.Unsubscribe<TournamentSetupRequestedSignal>(StartTournament);
        signalBus.Unsubscribe<RoundCompletedSignal>(AddPlayerToNextRound);
    }

    private void SetParticipantsData()
    {
        participants.Clear();

        int tournamentSize = gameConfigSO.TournamentSize;

        HelperUtilities.Shuffle(participantSO.DefaultNames);

        for (int i = 0; i < tournamentSize; i++)
        {
            participants.Add(new PlayerRoundData(i, participantSO.DefaultNames[i], 0, false));
        }
    }

    private void StartTournament()
    {
        byeParticipants.Clear();

        if (!IsPowerOfTwo(participants.Count))
        {
            int nextPower = NextPowerOfTwo(participants.Count);
            int byeParticipantsCount = nextPower - participants.Count;

            for (int i = 0; i < byeParticipantsCount; i++)
            {
                int random = UnityEngine.Random.Range(0, participants.Count);
                byeParticipants.Add(participants[random]);
                participants.RemoveAt(random);
            }
        }

        CreateRoundMatchup();
    }

    private void CreateRoundMatchup()
    {
        matchups.Clear();

        while (participants.Count > 1)
        {
            MatchupData matchup = new();

            int randomP1 = UnityEngine.Random.Range(0, participants.Count);
            matchup.playerOne = participants[randomP1];
            participants.RemoveAt(randomP1);

            int randomP2 = UnityEngine.Random.Range(0, participants.Count);
            matchup.playerTwo = participants[randomP2];
            participants.RemoveAt(randomP2);

            matchups.Enqueue(matchup);
        }
    }


    /*
    Developer's Note:
    If you've really looked into this far, you must be curious about the project. First of all, thank you for your interest. 
    I know this two methods (Method_A and Method_B) and flow could be coded better, but I got bored and had to focus on other projects. 
    That's why I left it like this. If you have a better solution, feel free to share it, I'll definitely be interested.
    */

    //Method_A
    private void AddPlayerToNextRound(RoundCompletedSignal signal)
    {
        participants.Add(signal.Player);

        if (matchups.Count <= 0)
        {
            EvaluateTournamentProgress();
        }

        if (matchups.Count <= 0 && participants.Count == 1) return;

        signalBus.Fire(new TournamentProgressedSignal(signal.Player.Name));
    }

    //Method_B
    private void EvaluateTournamentProgress()
    {
        if (byeParticipants.Count > 0)
        {
            foreach (var p in byeParticipants)
            {
                participants.Add(p);
            }

            byeParticipants.Clear();
        }

        else if (participants.Count == 1)
        {
            signalBus.Fire(new TournamentCompletedSignal(participants[0].Name));
            return;
        }

        CreateRoundMatchup();
    }

    public MatchupData GetNextMatch()
    {
        return matchups.Dequeue();
    }

    public MatchupData ShowNextMatch()
    {
        return matchups.Peek();
    }

    #region Helpers

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
