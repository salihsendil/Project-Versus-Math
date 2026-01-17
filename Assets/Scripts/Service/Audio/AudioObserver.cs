using System;
using Zenject;

public class AudioObserver : IInitializable, IDisposable
{
    //References
    [Inject] private SignalBus signalBus;
    [Inject] private SoundDataSO soundData;
    [Inject] private AudioService audioService;

    public void Initialize()
    {
        signalBus.Subscribe<OperationsSelectionChangedSignal>(OnOperationSelectionChanged);
        signalBus.Subscribe<LobbySetupRequestedSignal>(OnLobbySetupRequested);
        signalBus.Subscribe<AnswerEvaluationResultSignal>(OnAnswerEvaluationResultSignal);
        signalBus.Subscribe<TournamentProgressedSignal>(OnTournamentProgressed);
        signalBus.Subscribe<TournamentCompletedSignal>(OnTournamentComplete);

    }

    public void Dispose()
    {
        signalBus.Unsubscribe<OperationsSelectionChangedSignal>(OnOperationSelectionChanged);
        signalBus.Unsubscribe<LobbySetupRequestedSignal>(OnLobbySetupRequested);
        signalBus.Unsubscribe<AnswerEvaluationResultSignal>(OnAnswerEvaluationResultSignal);
        signalBus.Unsubscribe<TournamentProgressedSignal>(OnTournamentProgressed);
        signalBus.Unsubscribe<TournamentCompletedSignal>(OnTournamentComplete);
    }

    private void OnOperationSelectionChanged(OperationsSelectionChangedSignal signal)
    {
        var audioClip = signal.IsAllowed ? soundData.buttonClickPositive : soundData.buttonClickNegative;
        audioService.PlaySfx(audioClip);
    }

    private void OnLobbySetupRequested()
    {
        audioService.PlaySfx(soundData.loadLobby);
    }

    private void OnAnswerEvaluationResultSignal(AnswerEvaluationResultSignal signal)
    {
        var audioClip = signal.IsCorrect ? soundData.correctAnswer : soundData.wrongAnswer;
        audioService.PlaySfx(audioClip);
    }

    private void OnTournamentProgressed()
    {
        audioService.PlaySfx(soundData.roundEnd);
    }

    private void OnTournamentComplete()
    {
        audioService.PlaySfx(soundData.tournamentEnd);
    }
}
