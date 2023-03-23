using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] CuttingCounter cuttingCounter;
    private Animator animator;
    
    const string CUT = "Cut";
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounterOnOnCut;
    }

    private void CuttingCounterOnOnCut(object sender, EventArgs e)
    {
        animator.SetTrigger(CUT);
    }

}
