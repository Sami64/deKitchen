using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler OnCut;
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeArray;

    private int cuttingProgress;
    
    public override void Interact(Player player)
    {
        // Counter has no object
        if (!HasKitchenObject())
        {
            // Player carrying object
            if (player.HasKitchenObject())
            {
                // Player has object that can be cut
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO))
                {
                    // move object to counter
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    
                    var cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO);
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                    
                }
            }
        }
        // Counter has object
        else
        {
            // player carrying no object
            if (!player.HasKitchenObject())
            {
                // move counter object to player
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO))
        {
            cuttingProgress++;
            var cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO);
            OnCut?.Invoke(this, EventArgs.Empty);
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                // Get cut version of object
                var outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO);
                
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        var cuttingRecipeSO = GetCuttingRecipeSOWithInput(kitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        var cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO.output;
    }

    CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var cuttingRecipeSO in cuttingRecipeArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
