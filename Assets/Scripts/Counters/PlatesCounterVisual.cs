using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField]
    private PlatesCounter platesCounter;

    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private Transform counterTopPoint;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        platesCounter.OnPlateSpawned += OnPlateSpawned;
        platesCounter.OnPlateRemoved += OnPlateRemoved;
    }

    private void OnPlateSpawned(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        const float plateOffsetY = .1f;

        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

    private void OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count() - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

}
