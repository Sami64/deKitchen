using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform targetPoint;

    public static event EventHandler AnyObjectPlacedHere;

    private KitchenObject kitchenObject;

    public static void ResetStaticData()
    {
        AnyObjectPlacedHere = null;
    }

    public virtual void Interact(Player player)
    {
        Debug.Log("BaseCounter Interact");
    }

    public virtual void InteractAlternate(Player player)
    {
        // Debug.Log("BaseCounter InteractAlternate");
    }

    public Transform GetKitchenObjectFollowTransform() => targetPoint;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            AnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() => kitchenObject;

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject() => kitchenObject != null;
}
