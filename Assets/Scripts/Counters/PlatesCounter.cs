using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public EventHandler OnPlateSpawned;
    public EventHandler OnPlateRemoved;


    private const float spawnTimerMax = 4f;
    private float spawnTimer;
    private int platesSpawnedAmount;
    private const int platesSpawnedAmountMax = 4;

    [SerializeField]
    private KitchenObjectSO plateKitchenObjectSO;

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTimerMax)
        {
            spawnTimer = 0f;

            if (platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;
                
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (platesSpawnedAmount > 0)
            {
                platesSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
