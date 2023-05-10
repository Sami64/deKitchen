using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler AnyObjectTrashed;

    new public static void ResetStaticData()
    {
        AnyObjectTrashed = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            AnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
