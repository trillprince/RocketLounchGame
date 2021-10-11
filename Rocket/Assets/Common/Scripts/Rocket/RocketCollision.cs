using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketCollision
    {
        private readonly GameStateController _gameStateController;
        private int _collisionsLeft = 2;
        private List<Collider> _collisionList;

        public RocketCollision(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            _collisionList = new List<Collider>(_collisionsLeft);
        }

        public void ApplyCollision(Collider collider)
        {
            if(_collisionList.Contains(collider)) return;
            AddCollisionToList(collider);
            if (_collisionsLeft > 1)
            {
                _collisionsLeft -= 1;
                return;
            }
            _collisionsLeft = 0;
            _gameStateController.SetGameState(GameState.EndOfGame);
        }

        private void AddCollisionToList(Collider collider)
        {
            _collisionList.Add(collider);
        }
    }
}