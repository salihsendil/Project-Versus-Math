using System;
using System.Threading.Tasks;
using Zenject;

public class PlayerRoundData
{
    public int ID;
    public string Name;
    public int Score;
    public bool HasAnswered;

    public PlayerRoundData(int id, string name, int score, bool hasAnswered)
    {
        ID = id;
        Name = name;
        Score = score;
        HasAnswered = hasAnswered;
    }
}

public class RoundManager : IInitializable, IDisposable
{
    [Inject] private SignalBus signalBus;
    [Inject] private GameConfigSO gameConfigSO;
    [Inject] private QuestionFactory questionFactory;
    [Inject] private TournamentInstaller tournamentInstaller;

    //Players
    PlayerRoundData player1;
    PlayerRoundData player2;

    //Question
    QuestionData currentQuestion;
    int roundQuestionCounter;

    public void Initialize()
    {
        signalBus.Subscribe<StartRoundRequestSignal>(OnStartRoundRequest);
        signalBus.Subscribe<PlayerAnswerSubmitted>(OnPlayerAnswerSubmitted);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<StartRoundRequestSignal>(OnStartRoundRequest);
        signalBus.Unsubscribe<PlayerAnswerSubmitted>(OnPlayerAnswerSubmitted);
    }

    private void OnStartRoundRequest(StartRoundRequestSignal signal)
    {
        roundQuestionCounter = 0;
        MatchupData matchup = tournamentInstaller.GetNextMatch();
        player1 = matchup.playerOne;
        player2 = matchup.playerTwo;
        signalBus.Fire(new RoundDataReadySignal(matchup));
        OnGenerateQuestion();
    }

    private void OnGenerateQuestion()
    {
        roundQuestionCounter++;
        currentQuestion = questionFactory.GetNewQuestion();
        signalBus.Fire(new QuestionGeneratedSignal(currentQuestion));
    }

    private async void OnPlayerAnswerSubmitted(PlayerAnswerSubmitted signal)
    {
        signal.Player.HasAnswered = true;

        if (IsAnswerCorrect(signal.Answer))
        {
            signal.Player.Score++;

            signalBus.Fire(new AnswerEvaluationResultSignal(signal.Player, signal.Answer, true));

            await Task.Delay(1000);

            var result = CheckRoundStatus();

            if (result.isOver)
            {
                signalBus.Fire(new RoundCompletedSignal(result.winner));
                return;
            }

            OnGenerateQuestion();
            return;
        }

        signalBus.Fire(new AnswerEvaluationResultSignal(signal.Player, signal.Answer, false));

        if (player1.HasAnswered && player2.HasAnswered)
        {
            await Task.Delay(1000);

            OnGenerateQuestion();
        }
    }

    private bool IsAnswerCorrect(int answer) => answer == currentQuestion.CorrectAnswer;

    private (bool isOver, PlayerRoundData winner) CheckRoundStatus()
    {
        if (roundQuestionCounter < gameConfigSO.QuestionCountPerRound || player1.Score == player2.Score)
        {
            return (false, null);
        }

        PlayerRoundData winner = player1.Score > player2.Score ? player1 : player2;
        return (true, winner);
    }
}
