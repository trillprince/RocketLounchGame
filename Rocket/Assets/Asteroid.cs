using System.Collections;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.Satellite;
using UnityEngine;

public class Asteroid : MonoBehaviour,IAsteroid
{
    private RocketMovementController _rocketMovementController;
    private GameStateController _gameStateController;
    private ISpaceObjectController _spaceObjectController;
    private GameLoopController _gameLoopController;
    private IMoveComponent _asteroidMove;


    public void Constructor(RocketMovementController rocketMovementController, GameStateController gameStateController,
        ISpaceObjectController spaceObjectController, GameLoopController gameLoopController)
    {
        _rocketMovementController = rocketMovementController;
        _gameStateController = gameStateController;
        _spaceObjectController = spaceObjectController;
        _gameLoopController = gameLoopController;

        _asteroidMove = new AsteroidMove(rocketMovementController,transform);
    }
    
    public void Execute()
    {
        _asteroidMove.Move();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Transform GetTransform()
    {
        return transform;
    }

}