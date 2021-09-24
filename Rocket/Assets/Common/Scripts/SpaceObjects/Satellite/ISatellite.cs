using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;

public interface ISatellite: ISpaceObject
{
    public void SetFinalDeliveryStatus();
    public bool HasCargo();    
    public void Constructor(RocketMovementController rocketMovementController,
        GameStateController gameStateController,ISatelliteController satelliteController,GameLoopController gameLoopController);
}