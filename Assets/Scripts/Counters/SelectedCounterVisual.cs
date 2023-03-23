using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
    }

    private void PlayerOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        foreach (var visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
       
    }
    
    void Hide()
    {
        foreach (var visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
    
}
