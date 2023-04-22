using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;

    [SerializeField] private Transform recipeTemplate;
    // Start is called before the first frame update
    void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.RecipeSpawned += OnRecipeSpawned;
        DeliveryManager.Instance.RecipeCompleted += OnRecipeCompleted;
    }

    private void OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if(child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (var recipe in DeliveryManager.Instance.GetPendingRecipeList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipe);
        }
    }
}
