using System.Collections.Generic;

namespace Common.Scripts.Rocket
{
    public class RocketInventory
    {
        private Queue<ICollectable> _collectables = new Queue<ICollectable>();

        public void AddCollectable(ICollectable collectable)
        {
            _collectables.Enqueue(collectable);
        }
    }
}