using System;
using Common.Scripts.Cargo;
using Common.Scripts.MissionSystem;
using Common.Scripts.SpaceObjects;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class RocketCargo
    {
        private readonly Transform _transform;
        private GameObject _cargoPrefab;
        private ObjectPool _objectPool;

        public RocketCargo(ObjectPoolStorage objectPoolStorage,AssetProvider assetProvider,Transform transform)
        {
            _transform = transform;
        }

        
    }
}