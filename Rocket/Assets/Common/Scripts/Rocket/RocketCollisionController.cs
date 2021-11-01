using System;
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
            if (other.GetComponent<SpaceObject>() != null)
            {
                ApplyCollision(other);
            }
        }
    }
    
}