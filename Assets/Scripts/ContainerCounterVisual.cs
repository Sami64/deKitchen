using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] ContainerCounter containerCounter;
    private Animator animator;
    
    const string OPENCLOSE = "OpenClose";
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnContainerCounterInteract += ContainerCounterOnOnContainerCounterInteract;
    }

    private void ContainerCounterOnOnContainerCounterInteract(object sender, EventArgs e)
    {
        animator.SetTrigger(OPENCLOSE);
    }
}
