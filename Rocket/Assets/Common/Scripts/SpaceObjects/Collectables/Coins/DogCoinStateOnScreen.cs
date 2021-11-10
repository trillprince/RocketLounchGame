using Common.Scripts.MissionSystem;
using UnityEngine;

namespace Common.Scripts.SpaceObjects.Collectables.Coins
{
    public class DogCoinStateOnScreen: StateOnScreenPosition
    {
        public DogCoinStateOnScreen(Transform transform, ISpaceObjectLifeCycle spaceObjectLifeCycle, 
            DogCoinCollectable dogCoinCollectable): base(transform,spaceObjectLifeCycle,dogCoinCollectable)
        {
            
        }
    }
}