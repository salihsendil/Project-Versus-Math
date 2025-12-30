using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{

    [SerializeField] private SceneService sceneService;
    [SerializeField] private GameConfigSO gameConfigSO;
    [SerializeField] private TournamentInstaller tournamentInstaller;

    [SerializeField] private ParticipantSO tournamentData;

    public override void InstallBindings()
    {
        // ScriptableObject'i her yerden eriþilebilir hale getiriyoruz
        Container.BindInstance(gameConfigSO).AsSingle();
        Container.BindInstance(sceneService).AsSingle();
        Container.BindInstance(tournamentData).AsSingle();

        // Manager'ý bind ediyoruz ve baþlangýçta SO'yu ona paslýyoruz
        Container.Bind<TournamentInstaller>().FromComponentInNewPrefab(tournamentInstaller).AsSingle().NonLazy();
    }
}