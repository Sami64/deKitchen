using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    

    public override void Interact(Player player)
    {
        // Counter is empty
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        // Counter isn't empty
        else
        {
            // Player is carrying an object
            if (player.HasKitchenObject())
            {
                // Check if player is holding a plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Add object on counter to plate if same type not present
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO))
                    {
                        // Remove object from counter
                        GetKitchenObject().DestroySelf();
                    }
                }
                // Player isn't holding plate but something else
                else
                {
                    // Check if a plate is on the counter
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // Try to add object in player's hand to plate on counter if valid object
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO))
                        {
                            // Remove object from player
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            // Player has no object
            else
            {
                // Put counter's object on player
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
