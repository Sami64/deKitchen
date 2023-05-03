
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.RecipeSuccess += OnRecipeSuccess;
        DeliveryManager.Instance.RecipeFailed += OnRecipeFailed;
        
        CuttingCounter.AnyCut += CuttingCounterOnAnyCut;
        
        Player.Instance.PickedSomething += PlayerOnPickedSomething;
        
        BaseCounter.AnyObjectPlacedHere += BaseCounterOnAnyObjectPlacedHere;
        TrashCounter.AnyObjectTrashed += TrashCounterOnAnyObjectTrashed;
    }

    private void TrashCounterOnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounterOnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void PlayerOnPickedSomething(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounterOnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }
    
    void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    

    void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }

    
}
