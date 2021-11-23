using System;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects.Asteroid;
using Common.Scripts.SpaceObjects.Collectables.Coins;
using UnityEngine;

public class HologramCollisionBehaviour : IRocketCollisionBehaviour
{
    private readonly IEffectAudio _effectAudio;

    public HologramCollisionBehaviour(IEffectAudio effectAudio)
    {
        _effectAudio = effectAudio;
    }

    public void Collide(Collider collider, Action<Collider> applyCollision)
    {
        if (collider.GetComponent<SpaceObject>() is DogCoinCollectable)
        {
            applyCollision?.Invoke(collider);
        }
        else if (collider.GetComponent<SpaceObject>() is Asteroid)
        {
            _effectAudio.PlayFxAudioClip();
        }
    }
}