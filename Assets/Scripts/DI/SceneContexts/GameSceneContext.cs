using Zenject;

public class GameSceneContext : MonoInstaller
{
    public override void InstallBindings()
    {
        InterfaceBindings();
        SignalBindings();
    }

    private void SignalBindings()
    {
        Container.DeclareSignal<StartRoundRequestSignal>();
        Container.DeclareSignal<RoundDataReadySignal>();
        Container.DeclareSignal<QuestionGeneratedSignal>();
        Container.DeclareSignal<PlayerAnswerSubmitted>();
        Container.DeclareSignal<AnswerEvaluationResultSignal>();
        Container.DeclareSignal<NextRoundRequestSignal>();
    }

    private void InterfaceBindings()
    {
        Container.BindInterfacesAndSelfTo<RoundManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<QuestionFactory>().AsSingle();
    }
}