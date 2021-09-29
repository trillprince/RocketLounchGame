using Common.Scripts.UI;
using UnityEngine;

namespace Common.Scripts.Infrastructure
{
    public class BootStrapFactory: ITempFactory
    {
        private readonly IAssetProvider _assetProvider;
        
        public BootStrapFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameStateMachine CreateStateMachine()
        {
            return new GameStateMachine(new SceneLoader(_assetProvider.InstantiateForComponent<CoroutineRunner>(AssetPath.CoroutineRunner)),
                _assetProvider.InstantiateForComponent<LoadingCurtain>(AssetPath.LoadingCurtain));
        }

    }
}