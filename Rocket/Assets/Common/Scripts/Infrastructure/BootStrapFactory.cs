using Common.Scripts.UI;
using UnityEngine;

namespace Common.Scripts.Infrastructure
{
    public class BootStrapFactory: ITempFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly string CoroutineRunnerPath = "Prefabs/Bootstrap/CoroutineRunner";
        private readonly string CurtainPath = "Prefabs/UI/LoadingCurtain";

        public BootStrapFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameStateMachine CreateStateMachine()
        {
            return new GameStateMachine(new SceneLoader(InstantiateForComponent<CoroutineRunner>(CoroutineRunnerPath)),InstantiateForComponent<LoadingCurtain>(CurtainPath));
        }
        
        private T InstantiateForComponent <T> (string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab).GetComponent<T>();
        }
    }
}