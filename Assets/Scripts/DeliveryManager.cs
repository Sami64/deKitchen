using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler RecipeSpawned;
    public event EventHandler RecipeCompleted;
    public event EventHandler RecipeSuccess;
    public event EventHandler RecipeFailed;

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> pendingRecipesSOList;
    private int pendingRecipeMax = 4;

    private float spawnTimer;
    private float spawnTimerMax = 4f;
    

    private void Awake()
    {
        Instance = this;
        pendingRecipesSOList = new List<RecipeSO>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0f)
        {
            spawnTimer = spawnTimerMax;
            if(pendingRecipesSOList.Count < pendingRecipeMax)
            {
                var recipe = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                pendingRecipesSOList.Add(recipe);
                RecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < pendingRecipesSOList.Count; i++)
        {
            var recipe = pendingRecipesSOList[i];

            if (recipe.kitchenObjectSOList.Count == plateKitchenObject.GetPlateKitchenObjectsSO().Count)
            {
                // Has same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach (var pendingRecipeKitchenObjectSO in recipe.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    // Cycle through each kitchen object in current recipe
                    foreach (var plateKitchenObjectSO in plateKitchenObject.GetPlateKitchenObjectsSO())
                    {
                        // Cycle through each kitchen object on plate
                        if (plateKitchenObjectSO == pendingRecipeKitchenObjectSO)
                        {
                            // Match
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        // Ingredient is not on plate
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    // Ingredients match
                    // Remove from pending
                    pendingRecipesSOList.RemoveAt(i);
                    RecipeCompleted?.Invoke(this, EventArgs.Empty);
                    RecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        RecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetPendingRecipeList() => pendingRecipesSOList;
}
