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
    private IAsteroidController _asteroidController;
    private GameLoopController _gameLoopController;
    private IMoveComponent _asteroidMove;
    private AsteroidStateOnScreen _asteroidState;


    public void Constructor(RocketMovementController rocketMovementController, GameStateController gameStateController,
        IAsteroidController asteroidController, GameLoopController gameLoopController)
    {
        _rocketMovementController = rocketMovementController;
        _gameStateController = gameStateController;
        _asteroidController = asteroidController;
        _gameLoopController = gameLoopController;

        _asteroidMove = new AsteroidMove(rocketMovementController,transform);
        _asteroidState = new AsteroidStateOnScreen(GetComponent<MeshCollider>(), transform, asteroidController,
            gameLoopController);
    }
    
    public void Execute()
    {
        _asteroidMove.Move();
        _asteroidState.StateCheck();
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