using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GameConfigSO gameConfigSO;
    [SerializeField] private ParticipantSO participantSO;
    [SerializeField] private SceneService sceneServicePrefab;
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
    }

    private void PrefabBindings()
    {
        Container.Bind<SceneService>().FromComponentInNewPrefab(sceneServicePrefab).AsSingle().NonLazy();
        Container.Bind<CanvasGroup>().FromComponentInNewPrefab(loadingCanvasPrefab).AsSingle().NonLazy();
    }

    private void InterfaceBindings()
    {
        Container.BindInterfacesAndSelfTo<TournamentInstaller>().AsSingle();
    }

    private void SignalBusBindings()
    {
        Container.DeclareSignal<LobbySetupRequestedSignal>();
    }
}
