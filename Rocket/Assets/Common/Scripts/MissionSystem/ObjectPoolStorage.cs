using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class ObjectPoolStorage
    {
        private readonly Dictionary<string, ObjectPool> poolsDict = new Dictionary<string, ObjectPool>(12);

        public ObjectPool GetPool(GameObject prefab)
        {
            if (!poolsDict.TryGetValue(prefab.name, out ObjectPool objectPool))
            {
                objectPool = new ObjectPool(prefab);
                poolsDict[prefab.name] = objectPool;
            }
            return objectPool;
        }
        
    }
}