using System.Collections;
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
        public IRocketCollisionBehaviour RocketCollisionBehaviour { get; set; }
        private IRocketCollisionBehaviour _defaultCollisionBehaviour;

        private void Awake()
        {
            _defaultCollisionBehaviour = new DefaultRocketCollisionBehaviour();
            RocketCollisionBehaviour = _defaultCollisionBehaviour;
        }

        private void Start()
        {
            _collisionList = new Queue<Collider>();
        }

        private void ApplyCollision(Collider collider)
        {
            
            AddCollisionToList(collider);
            collider.GetComponent<SpaceObject>().Interact();
        }

        private void AddCollisionToList(Collider collider)
        {
            _collisionList.Enqueue(collider);
        }

        private void OnTriggerEnter(Collider other)
        {
            RocketCollisionBehaviour.Collide(other, ApplyCollision);
        }

        private void DefaultCollisionAction(Collider other)
        {
            if (other.GetComponent<SpaceObject>() != null)
            {
                ApplyCollision(other);
            }
        }

        public void SetCollisionBehaviorToDefault()
        {
            RocketCollisionBehaviour = _defaultCollisionBehaviour;
        }
    }
}