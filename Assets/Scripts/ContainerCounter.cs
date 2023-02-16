using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnContainerCounterInteract;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            Transform objectPrefab = Instantiate(kitchenObjectSO.prefab);
            objectPrefab.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnContainerCounterInteract?.Invoke(this, EventArgs.Empty);
        }
    }
}
