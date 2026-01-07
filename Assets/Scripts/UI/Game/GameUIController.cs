using UnityEngine;
using Zenject;

public class GameUIController : MonoBehaviour
{
    [Inject] private SignalBus signalBus;
    [Inject] private SceneService sceneService;

    [SerializeField] private PreparationUIView preparationUI;
    [SerializeField] private GameplayUIView gameplayUI;
    [SerializeField] private ResultUIView resultUI;
    [SerializeField] private TournamentEndUIView tournamentEndUI;

    private void OnEnable()
    {
        signalBus.Subscribe<QuestionGeneratedSignal>(OnQuestionGenerated);
        signalBus.Subscribe<RoundDataReadySignal>(OnRoundDataReady);
        signalBus.Subscribe<PlayerAnswerSubmitted>(OnPlayerAnswerSubmitted);
        signalBus.Subscribe<AnswerEvaluationResultSignal>(OnAnswerEvaluationResult);
        signalBus.Subscribe<TournamentProgressedSignal>(OnRoundCompleted);
        signalBus.Subscribe<NextRoundRequestSignal>(OnNextRoundRequest);
        signalBus.Subscribe<TournamentCompletedSignal>(OnTournamentCompleted);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<QuestionGeneratedSignal>(OnQuestionGenerated);
        signalBus.Unsubscribe<RoundDataReadySignal>(OnRoundDataReady);
        signalBus.Unsubscribe<PlayerAnswerSubmitted>(OnPlayerAnswerSubmitted);
        signalBus.Unsubscribe<AnswerEvaluationResultSignal>(OnAnswerEvaluationResult);
        signalBus.Unsubscribe<TournamentProgressedSignal>(OnRoundCompleted);
        signalBus.Unsubscribe<NextRoundRequestSignal>(OnNextRoundRequest);
        signalBus.Unsubscribe<TournamentCompletedSignal>(OnTournamentCompleted);
    }

    private void OnRoundDataReady(RoundDataReadySignal signal)
    {
        gameplayUI.SetPlayersData(signal.Matchup);
        gameplayUI.SetVisibility(true);
    }

    private void OnQuestionGenerated(QuestionGeneratedSignal signal)
    {
        gameplayUI.SetPlayersQuestion(signal.Question);
    }

    private void OnPlayerAnswerSubmitted(PlayerAnswerSubmitted signal)
    {
        gameplayUI.DisablePlayerUIButtons(signal.Player);
    }

    private void OnAnswerEvaluationResult(AnswerEvaluationResultSignal signal)
    {
        gameplayUI.FinalizePlayersButtons(signal.Player, signal.Answer, signal.IsCorrect);
    }

    private void OnRoundCompleted(TournamentProgressedSignal signal)
    {
        gameplayUI.SetVisibility(false);
        resultUI.SetVisibility(true);
        resultUI.SetScreen(signal.PlayerName);
    }

    private void OnNextRoundRequest()
    {
        preparationUI.SetVisibility(true);
    }

    private void OnTournamentCompleted(TournamentCompletedSignal signal)
    {
        sceneService.FadeToTournamentEnd(() =>
        {
            gameplayUI.SetVisibility(false);
            resultUI.SetVisibility(false);

            tournamentEndUI.SetScreen(signal.WinnerName);
            tournamentEndUI.SetVisibility(true);
        });
    }

}
