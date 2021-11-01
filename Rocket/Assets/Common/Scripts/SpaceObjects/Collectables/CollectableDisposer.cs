using Common.Scripts.MissionSystem;
using Common.Scripts.SpaceObjects;

public class CollectableDisposer: ICollectableDisposer
{
    private readonly ISpaceObjectLifeCycle _spaceObjectLifeCycle;
    private readonly ISpaceObject _spaceObject;

    public CollectableDisposer(ISpaceObjectLifeCycle spaceObjectLifeCycle,ISpaceObject spaceObject)
    {
        _spaceObjectLifeCycle = spaceObjectLifeCycle;
        _spaceObject = spaceObject;
    }

    public void DisposeCollectable()
    {
        _spaceObjectLifeCycle.Dispose(_spaceObject);
    }
}