using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;

public interface IAsteroid: ISpaceObject
{
    public void Constructor(RocketMovementController rocketMovementController, GameStateController gameStateController,
        ISpaceObjectController spaceObjectController, GameLoopController gameLoopController);
}