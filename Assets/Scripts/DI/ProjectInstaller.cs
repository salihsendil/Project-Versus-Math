using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SceneService sceneService;
    [SerializeField] private ParticipantSO participantsSO;
    [SerializeField] private GameConfigSO gameConfigSO;
    [SerializeField] private TournamentInstaller tournamentInstaller;
    [SerializeField] private CanvasGroup loadingScreen;

    public override void InstallBindings()
    {
        Container.Bind<CanvasGroup>()
            .FromComponentInNewPrefab(loadingScreen)
            .AsSingle()
            .NonLazy();

        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<LoadGameRequest>();
        Container.DeclareSignal<LoadLobbyRequest>();
        Container.DeclareSignal<LoadMainMenuRequest>();

        Container.BindInstance(participantsSO).AsSingle();
        Container.BindInstance(gameConfigSO).AsSingle();

        Container.BindInterfacesAndSelfTo<SceneService>()
            .FromInstance(sceneService)
            .AsSingle();
        Container.QueueForInject(sceneService);

        Container.BindInterfacesAndSelfTo<TournamentInstaller>()
            .FromComponentInNewPrefab(tournamentInstaller)
            .AsSingle()
            .NonLazy();
    }
}