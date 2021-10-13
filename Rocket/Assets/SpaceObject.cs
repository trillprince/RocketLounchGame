using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects;
using UnityEngine;

public class SpaceObject:MonoBehaviour,ISpaceObject,IInteractable
{
    private RocketController _rocketController;
    private ISpaceObjectController _spaceObjectController;
    private GameLoopController _gameLoopController;
    private GameStateController _gameStateController;
    private ISpawnPosition _spawnPosition;

    public virtual void Constructor(RocketController rocketController, ISpaceObjectController spaceObjectController,
        GameLoopController gameLoopController, GameStateController gameStateController, ISpawnPosition spawnPosition)
    {
        _rocketController = rocketController;
        _spaceObjectController = spaceObjectController;
        _gameLoopController = gameLoopController;
        _gameStateController = gameStateController;
        _spawnPosition = spawnPosition;
    }
    
    public Vector3 GetSpawnPosition()
    {
        return _spawnPosition.GetSpawnPosition();
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