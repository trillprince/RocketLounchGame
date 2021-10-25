using System.Collections;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class DataInstaller : MonoInstaller
{
    public PlayerData playerData;
    public override void InstallBindings()
    {
        InstallPlayerDataSaver();
    }

    private void InstallPlayerDataSaver()
    {
        Container.Bind<GameProgress>().FromInstance(new GameProgress(new PlayerDataSaver(playerData)));
    }
}
