using System;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects.Collectables.Coins;
using UnityEngine;

public class HologramCollisionBehaviour : IRocketCollisionBehaviour
{
    public void Collide(Collider collider, Action<Collider> applyCollision)
    {
        if (collider.GetComponent<SpaceObject>() is DogCoinCollectable)
        {
            applyCollision?.Invoke(collider);
        }
    }
}