using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start() 
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnIngredientAdded;
    }

    private void PlateKitchenObjectOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if(child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        
        foreach (var kitchenObjectSO in plateKitchenObject.GetPlateKitchenObjectsSO())
        {
            
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateSingleIconUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
