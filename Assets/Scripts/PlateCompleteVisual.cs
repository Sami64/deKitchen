using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject kitchenObjectGameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnIngredientAdded;
    }

    private void PlateKitchenObjectOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (var kitchenObjectSOGameObject in kitchenObjectSOGameObjects)
        {
            if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.kitchenObjectGameObject.gameObject.SetActive(true);
            }
        }
    }
}
