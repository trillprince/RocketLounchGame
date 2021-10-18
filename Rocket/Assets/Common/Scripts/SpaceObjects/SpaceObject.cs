using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects;
using UnityEngine;

public class SpaceObject:MonoBehaviour,ISpaceObject,IInteractable
{
    private RocketController _rocketController;
    private ISpaceObjectLifeCycle _spaceObjectLifeCycle;
    private GameLoopController _gameLoopController;
    private IGameStateController _gameStateController;
    private ISpawnPosition _spawnPosition;
    private Collider _collider;

    public virtual void Constructor(RocketController rocketController, ISpaceObjectLifeCycle spaceObjectLifeCycle,
        GameLoopController gameLoopController, IGameStateController gameStateController, ISpawnPosition spawnPosition)
    {
        _rocketController = rocketController;
        _spaceObjectLifeCycle = spaceObjectLifeCycle;
        _gameLoopController = gameLoopController;
        _gameStateController = gameStateController;
        _spawnPosition = spawnPosition;
        _collider = GetComponent<Collider>();
    }

    public Vector3 GetSpawnPosition()
    {
        return _spawnPosition.GetSpawnPosition(GetCollider());
    }

    private Collider GetCollider()
    {
        return _collider;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Transform GetTransform()
    {
        return transform;
    }


    public virtual void Execute()
    {
        
    }

    public virtual void Interact()
    {
       
    }
}