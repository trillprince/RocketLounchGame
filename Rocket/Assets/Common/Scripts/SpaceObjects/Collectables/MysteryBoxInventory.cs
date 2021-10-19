public class MysteryBoxInventory: IInventory
{
    public ICollectable GetCollectable()
    {
        return new MysteryBoxCollectable();
    }
}