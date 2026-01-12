using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GameConfigSO gameConfigSO;
    [SerializeField] private ParticipantSO participantSO;
    [SerializeField] private SoundDataSO soundDataSO;
    [SerializeField] private SceneService sceneServicePrefab;
    [SerializeField] private AudioService audioServicePrefab;
    [SerializeField] private CanvasGroup loadingCanvasPrefab;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        ScriptableObjectBinding();
        PrefabBindings();
        SignalBusBindings();
        InterfaceBindings();
    }

    private void ScriptableObjectBinding()
    {
        Container.BindInstance(gameConfigSO).AsSingle().NonLazy();
        Container.BindInstance(participantSO).AsSingle().NonLazy();
        Container.BindInstance(soundDataSO).AsSingle().NonLazy();
    }

    private void PrefabBindings()
    {
        Container.Bind<AudioService>().FromComponentInNewPrefab(audioServicePrefab).AsSingle().NonLazy();
        Container.Bind<SceneService>().FromComponentInNewPrefab(sceneServicePrefab).AsSingle().NonLazy();
        Container.Bind<CanvasGroup>().FromComponentInNewPrefab(loadingCanvasPrefab).AsSingle().NonLazy();
    }

    private void InterfaceBindings()
    {
        Container.BindInterfacesAndSelfTo<TournamentInstaller>().AsSingle();
        Container.BindInterfacesAndSelfTo<AudioObserver>().AsSingle().NonLazy();
    }

    private void SignalBusBindings()
    {
        Container.DeclareSignal<OperationsSelectionChangedSignal>();
        Container.DeclareSignal<OnUIValueChangesButtonSignal>();
        Container.DeclareSignal<LobbySetupRequestedSignal>();
        Container.DeclareSignal<TournamentSetupRequestedSignal>();
        Container.DeclareSignal<AnswerEvaluationResultSignal>();
        Container.DeclareSignal<RoundCompletedSignal>();
        Container.DeclareSignal<TournamentCompletedSignal>();
        Container.DeclareSignal<TournamentProgressedSignal>();
    }
}
