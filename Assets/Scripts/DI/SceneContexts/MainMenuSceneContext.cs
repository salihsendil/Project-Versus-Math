using System;
using UnityEngine;
using Zenject;

public class MainMenuSceneContext : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusBindings();
    }

    private void SignalBusBindings()
    {
        Container.DeclareSignal<OperationsSelectionChangedSignal>();
    }
}