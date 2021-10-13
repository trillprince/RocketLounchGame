using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.SpaceObjects;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class RocketCollisionController : MonoBehaviour
    {
        private Queue<Collider> _collisionList;


        [Inject]
        public void Constructor(GameStateController gameStateController)
        {
            _collisionList = new Queue<Collider>();
        }
        
        private void ApplyCollision(Collider collider)
        {
            if(_collisionList.Contains(collider) || collider.GetComponent<SpaceObject>() == null) return;
            AddCollisionToList(collider);
            collider.GetComponent<SpaceObject>().Interact();
        }

        private void AddCollisionToList(Collider collider)
        {
            _collisionList.Enqueue(collider);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            ApplyCollision(other);
        }
    }

    public interface IInteractable
    {
        public void Interact();
    }
}