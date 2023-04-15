using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    
    public enum State{
        Idle,
        Frying,
        Fried,
        Burned
    }
    [SerializeField]
    private FryingRecipeSO[] fryingRecipeSOArray;

    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private float fryingTimer;
    private float burningTimer;

    private State state;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                    
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        burningTimer = 0f;
                        state = State.Fried;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO);
                    }
                    break;
                case State.Fried :
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });
                    
                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
            
        }
    }

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
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO);
                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
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
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                {
                    progressNormalized = 0f
                });
            }
        }
    }
    
    bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        var fryingRecipeSO = GetFryingRecipeSOWithInput(kitchenObjectSO);
        return fryingRecipeSO != null;
    }

    KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        var fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO.output;
    }

    FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    
    BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

    
}
